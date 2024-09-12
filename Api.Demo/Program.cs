
using Api.Demo;
using Api.Demo.Middleware;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Web;
using SqlSugar;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// 添加控制器，处理时间格式
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // 处理日期格式
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
    // 处理返回的属性首字母大小写
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

// 添加验权
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options =>
{
    //取出私钥
    var secretByte = Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]);

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,

        //验证发布者
        ValidateIssuer = true,
        ValidIssuer = configuration["Authentication:Issuer"],

        //验证接收者
        ValidateAudience = true,
        ValidAudience = configuration["Authentication:Audience"],

        //验证是否过期
        ValidateLifetime = true,
        //验证私钥
        IssuerSigningKey = new SymmetricSecurityKey(secretByte),

        ClockSkew = TimeSpan.Zero
    };

});

// 注册上下文：AOP里面可以获取IOC对象
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    string server = configuration.GetValue<string>("ConnectionConfig:Server");
    string port = configuration.GetValue<string>("ConnectionConfig:Port");
    string dataBase = configuration.GetValue<string>("ConnectionConfig:Database");
    string uid = configuration.GetValue<string>("ConnectionConfig:Uid");
    string pwd = configuration.GetValue<string>("ConnectionConfig:Pwd");

    SqlSugarClient sqlSugar = new(new ConnectionConfig()
    {
        DbType = DbType.MySql,
        ConnectionString = $"Server={server};Port={port};Database={dataBase};Uid={uid};Pwd={pwd};",
        IsAutoCloseConnection = true,
        InitKeyType = InitKeyType.Attribute,
    });


    sqlSugar.Aop.OnLogExecuting = (sql, pars) =>
    {
        // 在这里添加你的日志记录代码，例如：
        Console.WriteLine($"SQL: {sql}, Parameters: {JsonConvert.SerializeObject(pars)}");
    };

    return sqlSugar;
});

// 添加日志
builder.Services.AddLogging(logBuilder =>
{
    logBuilder.ClearProviders();// 删除所有其他的关于日志记录的配置
    logBuilder.SetMinimumLevel(LogLevel.Trace);// 设置最低的log级别
    logBuilder.AddNLog("NLog.config");// 支持nlog
});

// 添加swagger
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                    }
                },new string[] { }
            }
        }
    );
});


// 添加autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        // 在这里向Autofac容器注册服务
        containerBuilder.RegisterModule<AutofacModule>();
    });

var app = builder.Build();

// 添加异常处理
app.UseMiddleware<ExceptionMiddleware>();

//添加jwt验证  这2句千万不能忘记了，顺序不能颠倒。
app.UseAuthentication();
//代码从上到下执行，中间可以加判断， 权限设置问题
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
