using Api.Model.SysManagement;
using Api.IService.SysManagement;
using SqlSugar;
using Microsoft.Extensions.Configuration;

namespace Api.Service.SysManagement
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        public readonly IConfiguration configuration;
        public SysUserService(ISqlSugarClient client, IConfiguration config) : base(client)
        {
            this.configuration = config;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserAccount">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SysUser> Login(string UserAccount, string PassWord)
        {
            var user = await Task.Run(() =>
                client.Queryable<SysUser>()
                .Where(x => x.UserAccount == UserAccount && x.PassWord == PassWord)
                .First()
            );

            return user is null ? throw new Exception("用户名或密码不正确！") : user;
        }
    }
}
