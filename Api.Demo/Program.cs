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
    SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = DbType.MySql,
        ConnectionString = $"Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123;",
        IsAutoCloseConnection = true,
    });
    return sqlSugar;
});

var app = builder.Build();

// ����Զ����м��
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();