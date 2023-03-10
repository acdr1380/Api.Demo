using Api.Demo;
using Api.Demo.Middleware;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

// 添加控制器
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});



//注册上下文：AOP里面可以获取IOC对象
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

// 添加自定义中间件
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();