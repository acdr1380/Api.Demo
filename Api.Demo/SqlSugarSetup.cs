using SqlSugar;

namespace Api.Demo
{
    public static class SqlSugarSetup
    {
        public static void AddSqlsugarSetup(this IServiceCollection service, IConfiguration configuration, string dbName = "test")
        {
            SqlSugarScope scope = new SqlSugarScope(new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = configuration.GetConnectionString(dbName),
                IsAutoCloseConnection = true,
            }, db =>
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);
                };
            });
            service.AddSingleton<ISqlSugarClient>(scope);
        }
    }
}
