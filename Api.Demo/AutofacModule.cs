using Autofac;
using System.Reflection;

namespace Api.Demo
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 注册指定程序集中的服务类
            builder.RegisterAssemblyTypes(Assembly.Load("Api.Service"))
                // 选择以 "Service" 结尾的类
                .Where(type => type.Name.EndsWith("Service"))
                // 排除以 "Base" 开头的类
                .Where(type => !type.Name.StartsWith("Base"))
                // 仅注册公共类
                .PublicOnly()
                // 确保类型为类
                .Where(type => type.IsClass)
                // 注册为实现的接口
                .AsImplementedInterfaces()
                // 设置生命周期（可选），例如：每个请求一个实例
                .InstancePerLifetimeScope();
        }
    }
}
