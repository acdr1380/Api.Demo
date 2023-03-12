using Api.Demo;
using Api.Demo.Middleware;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

// ��ӿ�����
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

//ע�������ģ�AOP������Ի�ȡIOC����
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    var config = builder.Configuration;
    string server = config.GetValue<string>("ConnectionConfig:Server");
    string port = config.GetValue<string>("ConnectionConfig:Port");
    string dataBase = config.GetValue<string>("ConnectionConfig:Database");
    string uid = config.GetValue<string>("ConnectionConfig:Uid");
    string pwd = config.GetValue<string>("ConnectionConfig:Pwd");
    SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = DbType.MySql,
        ConnectionString = $"Server={server};Port={port};Database={dataBase};Uid={uid};Pwd={pwd};",
        IsAutoCloseConnection = true,
    });
    return sqlSugar;
});

var app = builder.Build();

// ����Զ����м��
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
