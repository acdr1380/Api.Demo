using Api.Demo.Middleware;
using SqlSugar;
using Api.Common;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// ���log4net
builder.Logging.AddLog4Net("log4net.config");

// ��ӿ�����������ʱ���ʽ
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
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

var app = builder.Build();

// ����쳣����
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
