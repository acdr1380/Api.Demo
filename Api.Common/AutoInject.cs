using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Api.Common
{
    /// <summary>
    /// 自动依赖注入
    /// </summary>
    public static class AutoInject
    {
        /// <summary>
        /// 自动注入所有的程序集有InjectAttribute标签
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoDi(this IServiceCollection serviceCollection)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var assemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToList();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(a => a.GetCustomAttribute<AutoInjectAttribute>() != null)
                    .ToList();
                if (types.Count <= 0)
                    continue;
                foreach (var type in types)
                {
                    var attr = type.GetCustomAttribute<AutoInjectAttribute>();
                    if (attr?.Type == null)
                        continue;
                    switch (attr.InjectType)
                    {
                        case InjectType.Scope:
                            serviceCollection.AddScoped(attr.Type, type);
                            break;
                        case InjectType.Single:
                            serviceCollection.AddSingleton(attr.Type, type);
                            break;
                        case InjectType.Transient:
                            serviceCollection.AddTransient(attr.Type, type);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return serviceCollection;
        }
    }

    /// <summary>
    /// 注入类型
    /// </summary>
    public enum InjectType
    {
        Scope,
        Single,
        Transient
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoInjectAttribute : Attribute
    {
        public AutoInjectAttribute(Type interfaceType, InjectType injectType)
        {
            Type = interfaceType;
            InjectType = injectType;
        }

        public Type Type { get; set; }

        /// <summary>
        /// 注入类型
        /// </summary>
        public InjectType InjectType { get; set; }
    }

}
