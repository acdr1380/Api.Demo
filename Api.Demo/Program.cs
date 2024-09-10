using Api.Common;
using Api.Demo.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SqlSugar;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Api.Demo;

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

//注册上下文：AOP里面可以获取IOC对象
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

app.MapControllers();

app.Run();
