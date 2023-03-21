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

// 添加log4net
builder.Logging.AddLog4Net("log4net.config");

// 添加控制器，处理时间格式
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

//注册上下文：AOP里面可以获取IOC对象
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

// 注入服务
//builder.Services.AddSingleton<ISysUserService, SysUserService>();
// 自动注入
builder.Services.AddAutoDi();

//注册JWT服务
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true, //是否验证Issuer
//        ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"), // 发行人Issuer
//        ValidateAudience = true, //是否验证Audience
//        ValidAudience = configuration.GetValue<string>("Jwt:Audience"), //订阅人Audience
//        ValidateIssuerSigningKey = true, //是否验证SecurityKey
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt: SecretKey"))), //SecurityKey
//        ValidateLifetime = true, //是否验证失效时间
//        ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
//        RequireExpirationTime = true,
//    };
//});

builder.Services.AddSingleton(new JwtHelper(configuration));

var app = builder.Build();

//调用中间件：UseAuthentication（认证），必须在所有需要身份认证的中间件前调用，比如 UseAuthorization（授权）。
//app.UseAuthentication();
//app.UseAuthorization();

// 添加异常处理
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
