using Api.Model.SysManagement;
using Api.IService.SysManagement;
using SqlSugar;
using Api.Common;

namespace Api.Service.SysManagement
{
    [AutoInject(typeof(ISysUserService), InjectType.Scope)]
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {

        public SysUserService(ISqlSugarClient client) : base(client)
        {
        }

        public IEnumerable<SysUser> GetListPage(string key, int pageIndex, int pageSize, ref int total)
        {
            return client.Queryable<SysUser>().ToPageList(pageIndex, pageSize, ref total);
        }

        public async Task<SysUser> Login(string UserAccount, string PassWord)
        {
            var user = await Task.Run(() => client.Queryable<SysUser>().Where(x => x.UserAccount == UserAccount && x.PassWord == PassWord).First());

            return user is null ? throw new Exception("用户名或密码不正确！") : user;
        }
    }
}
