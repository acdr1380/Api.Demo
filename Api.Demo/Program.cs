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

// 获取配置信息
var configuration = builder.Configuration;

// 添加控制器，并设置 JSON 序列化选项
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // 设置日期格式
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
    // 设置属性序列化时首字母小写
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

// 添加 JWT 验证
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // 获取私钥
    var secretByte = Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]);

    options.RequireHttpsMetadata = false; // 是否需要 HTTPS
    options.SaveToken = true; // 保存令牌
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // 验证签名密钥
        ValidateIssuer = true, // 验证发行者
        ValidIssuer = configuration["Authentication:Issuer"], // 有效的发行者
        ValidateAudience = true, // 验证接收者
        ValidAudience = configuration["Authentication:Audience"], // 有效的接收者
        ValidateLifetime = true, // 验证令牌有效期
        IssuerSigningKey = new SymmetricSecurityKey(secretByte), // 签名密钥
        ClockSkew = TimeSpan.Zero // 令牌过期的宽限时间
    };
});

// 注册 HTTP 上下文访问器
builder.Services.AddHttpContextAccessor();

// 配置 SqlSugar 客户端
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    var server = configuration.GetValue<string>("ConnectionConfig:Server");
    var port = configuration.GetValue<string>("ConnectionConfig:Port");
    var database = configuration.GetValue<string>("ConnectionConfig:Database");
    var uid = configuration.GetValue<string>("ConnectionConfig:Uid");
    var pwd = configuration.GetValue<string>("ConnectionConfig:Pwd");

    // 创建 SqlSugar 客户端
    SqlSugarClient sqlSugar = new(new ConnectionConfig
    {
        DbType = DbType.MySql, // 数据库类型
        ConnectionString = $"Server={server};Port={port};Database={database};Uid={uid};Pwd={pwd};",
        IsAutoCloseConnection = true, // 自动关闭连接
        InitKeyType = InitKeyType.Attribute, // 属性初始化方式
    });

    // 添加 SQL 执行日志
    sqlSugar.Aop.OnLogExecuting = (sql, pars) =>
    {
        // 记录 SQL 执行信息
        // Console.WriteLine($"SQL: {sql}, Parameters: {JsonConvert.SerializeObject(pars)}");

    };

    return sqlSugar;
});

// 添加日志支持
builder.Services.AddLogging(logBuilder =>
{
    logBuilder.ClearProviders(); // 清除默认日志提供者
    logBuilder.SetMinimumLevel(LogLevel.Trace); // 设置日志级别
    logBuilder.AddNLog("NLog.config"); // 使用 NLog 配置文件
});

// 添加 Swagger 文档生成
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
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
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new string[] { }
        }
    });
});

// 配置 Autofac 作为依赖注入容器
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule<AutofacModule>(); // 注册 Autofac 模块
    });

var app = builder.Build();

// 添加异常处理中间件
app.UseMiddleware<ExceptionMiddleware>();

// 添加 JWT 验证中间件（顺序重要）
app.UseAuthentication();
app.UseAuthorization(); // 添加授权中间件

// 开发环境下启用 Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // 启用 Swagger 中间件
    app.UseSwaggerUI(); // 启用 Swagger UI
}

// 映射控制器
app.MapControllers();

// 启动应用
app.Run();
