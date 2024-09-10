using Autofac;
using System.Reflection;

namespace Api.Demo
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 程序集内所有具象类 
            builder.RegisterAssemblyTypes(Assembly.Load("Api.Service"))
            // 选择具有名称以 “Service” 结尾的类
            .Where(x => x.Name.EndsWith("Service"))
            // 不要以Base开头的类
            .Where(x => !x.Name.StartsWith("Base"))
            // 只要public访问权限的
            .PublicOnly()
            // 只要class型（主要为了排除值和interface类型） 
            .Where(x => x.IsClass)
            .AsImplementedInterfaces();
        }
    }
}
