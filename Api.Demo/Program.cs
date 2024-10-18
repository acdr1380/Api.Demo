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

// ��ȡ������Ϣ
var configuration = builder.Configuration;

// ��ӿ������������� JSON ���л�ѡ��
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // �������ڸ�ʽ
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
    // �����������л�ʱ����ĸСд
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

// ��� JWT ��֤
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // ��ȡ˽Կ
    var secretByte = Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]);

    options.RequireHttpsMetadata = false; // �Ƿ���Ҫ HTTPS
    options.SaveToken = true; // ��������
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // ��֤ǩ����Կ
        ValidateIssuer = true, // ��֤������
        ValidIssuer = configuration["Authentication:Issuer"], // ��Ч�ķ�����
        ValidateAudience = true, // ��֤������
        ValidAudience = configuration["Authentication:Audience"], // ��Ч�Ľ�����
        ValidateLifetime = true, // ��֤������Ч��
        IssuerSigningKey = new SymmetricSecurityKey(secretByte), // ǩ����Կ
        ClockSkew = TimeSpan.Zero // ���ƹ��ڵĿ���ʱ��
    };
});

// ע�� HTTP �����ķ�����
builder.Services.AddHttpContextAccessor();

// ���� SqlSugar �ͻ���
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    var server = configuration.GetValue<string>("ConnectionConfig:Server");
    var port = configuration.GetValue<string>("ConnectionConfig:Port");
    var database = configuration.GetValue<string>("ConnectionConfig:Database");
    var uid = configuration.GetValue<string>("ConnectionConfig:Uid");
    var pwd = configuration.GetValue<string>("ConnectionConfig:Pwd");

    // ���� SqlSugar �ͻ���
    SqlSugarClient sqlSugar = new(new ConnectionConfig
    {
        DbType = DbType.MySql, // ���ݿ�����
        ConnectionString = $"Server={server};Port={port};Database={database};Uid={uid};Pwd={pwd};",
        IsAutoCloseConnection = true, // �Զ��ر�����
        InitKeyType = InitKeyType.Attribute, // ���Գ�ʼ����ʽ
    });

    // ��� SQL ִ����־
    sqlSugar.Aop.OnLogExecuting = (sql, pars) =>
    {
        // ��¼ SQL ִ����Ϣ
        // Console.WriteLine($"SQL: {sql}, Parameters: {JsonConvert.SerializeObject(pars)}");

    };

    return sqlSugar;
});

// �����־֧��
builder.Services.AddLogging(logBuilder =>
{
    logBuilder.ClearProviders(); // ���Ĭ����־�ṩ��
    logBuilder.SetMinimumLevel(LogLevel.Trace); // ������־����
    logBuilder.AddNLog("NLog.config"); // ʹ�� NLog �����ļ�
});

// ��� Swagger �ĵ�����
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
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

// ���� Autofac ��Ϊ����ע������
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule<AutofacModule>(); // ע�� Autofac ģ��
    });

var app = builder.Build();

// ����쳣�����м��
app.UseMiddleware<ExceptionMiddleware>();

// ��� JWT ��֤�м����˳����Ҫ��
app.UseAuthentication();
app.UseAuthorization(); // �����Ȩ�м��

// �������������� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // ���� Swagger �м��
    app.UseSwaggerUI(); // ���� Swagger UI
}

// ӳ�������
app.MapControllers();

// ����Ӧ��
app.Run();
