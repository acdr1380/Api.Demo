using Api.Demo.Middleware;
using SqlSugar;
using Api.IService.SysManagement;
using Api.Service.SysManagement;
using Api.Common;

var builder = WebApplication.CreateBuilder(args);
// 添加控制器，处理时间格式
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

//注册上下文：AOP里面可以获取IOC对象
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    var config = builder.Configuration;
    string server = config.GetValue<string>("ConnectionConfig:Server");
    string port = config.GetValue<string>("ConnectionConfig:Port");
    string dataBase = config.GetValue<string>("ConnectionConfig:Database");
    string uid = config.GetValue<string>("ConnectionConfig:Uid");
    string pwd = config.GetValue<string>("ConnectionConfig:Pwd");
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


var app = builder.Build();

// 添加异常处理
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
