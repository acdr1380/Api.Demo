
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

// ��ӿ�����������ʱ���ʽ
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // �������ڸ�ʽ
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
    // �����ص���������ĸ��Сд
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

// �����Ȩ
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options =>
{
    //ȡ��˽Կ
    var secretByte = Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]);

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,

        //��֤������
        ValidateIssuer = true,
        ValidIssuer = configuration["Authentication:Issuer"],

        //��֤������
        ValidateAudience = true,
        ValidAudience = configuration["Authentication:Audience"],

        //��֤�Ƿ����
        ValidateLifetime = true,
        //��֤˽Կ
        IssuerSigningKey = new SymmetricSecurityKey(secretByte),

        ClockSkew = TimeSpan.Zero
    };

});

// ע�������ģ�AOP������Ի�ȡIOC����
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
        // ��������������־��¼���룬���磺
        Console.WriteLine($"SQL: {sql}, Parameters: {JsonConvert.SerializeObject(pars)}");
    };

    return sqlSugar;
});

// �����־
builder.Services.AddLogging(logBuilder =>
{
    logBuilder.ClearProviders();// ɾ�����������Ĺ�����־��¼������
    logBuilder.SetMinimumLevel(LogLevel.Trace);// ������͵�log����
    logBuilder.AddNLog("NLog.config");// ֧��nlog
});

// ���swagger
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
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


// ���autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        // ��������Autofac����ע�����
        containerBuilder.RegisterModule<AutofacModule>();
    });

var app = builder.Build();

// ����쳣����
app.UseMiddleware<ExceptionMiddleware>();

//���jwt��֤  ��2��ǧ���������ˣ�˳���ܵߵ���
app.UseAuthentication();
//������ϵ���ִ�У��м���Լ��жϣ� Ȩ����������
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
