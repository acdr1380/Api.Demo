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

// ��ӿ�����������ʱ���ʽ
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // �������ڸ�ʽ
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
    // �����ص���������ĸ��Сд
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
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

app.MapControllers();

app.Run();
