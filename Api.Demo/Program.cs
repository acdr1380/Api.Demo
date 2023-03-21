using Api.Demo.Middleware;
using SqlSugar;
using Api.IService.SysManagement;
using Api.Service.SysManagement;
using Api.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthenticationTest;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// ���log4net
builder.Logging.AddLog4Net("log4net.config");

// ��ӿ�����������ʱ���ʽ
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

//ע�������ģ�AOP������Ի�ȡIOC����
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    string server = configuration.GetValue<string>("ConnectionConfig:Server");
    string port = configuration.GetValue<string>("ConnectionConfig:Port");
    string dataBase = configuration.GetValue<string>("ConnectionConfig:Database");
    string uid = configuration.GetValue<string>("ConnectionConfig:Uid");
    string pwd = configuration.GetValue<string>("ConnectionConfig:Pwd");
    SqlSugarScope sqlSugar = new(new ConnectionConfig()
    {
        DbType = DbType.MySql,
        ConnectionString = $"Server={server};Port={port};Database={dataBase};Uid={uid};Pwd={pwd};",
        IsAutoCloseConnection = true,
    });
    return sqlSugar;
});

// ע�����
//builder.Services.AddSingleton<ISysUserService, SysUserService>();
// �Զ�ע��
builder.Services.AddAutoDi();

//ע��JWT����
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true, //�Ƿ���֤Issuer
//        ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"), // ������Issuer
//        ValidateAudience = true, //�Ƿ���֤Audience
//        ValidAudience = configuration.GetValue<string>("Jwt:Audience"), //������Audience
//        ValidateIssuerSigningKey = true, //�Ƿ���֤SecurityKey
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt: SecretKey"))), //SecurityKey
//        ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
//        ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
//        RequireExpirationTime = true,
//    };
//});

builder.Services.AddSingleton(new JwtHelper(configuration));

var app = builder.Build();

//�����м����UseAuthentication����֤����������������Ҫ�����֤���м��ǰ���ã����� UseAuthorization����Ȩ����
//app.UseAuthentication();
//app.UseAuthorization();

// ����쳣����
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
