using Api.Common;
using Api.IRepository.SysManagement;
using Api.Model.SysManagement;
using SqlSugar;

namespace Api.Repository.SysManagement
{
    [AutoInject(typeof(ISysUserRepository), InjectType.Scope)]
    public class SysUserRepository : BaseRepository<SysUser>, ISysUserRepository
    {
        public SysUserRepository(ISqlSugarClient _client) : base(_client)
        {
        }

        public IEnumerable<SysUser> GetListPage(string key, int pageIndex, int pageSize, ref int total)
        {
            return client.Queryable<SysUser>().ToPageList(pageIndex, pageSize, ref total);
        }

        public async Task<SysUser> Login(string UserAccount, string PassWord)
        {
            var a = client.Queryable<SysUser>().Where(x => x.UserAccount == UserAccount && x.PassWord == PassWord).ToList();
            var user = await Task.Run(() => client.Queryable<SysUser>().Where(x => x.UserAccount == UserAccount && x.PassWord == PassWord).ToList());
            if (user.Count == 0)
                throw new Exception("用户名或密码不正确！");
            return user.First();
        }
    }
}
