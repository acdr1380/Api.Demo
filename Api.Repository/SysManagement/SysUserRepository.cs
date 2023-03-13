using Api.IRepository.SysManagement;
using Api.Model.SysManagement;
using SqlSugar;

namespace Api.Repository.SysManagement
{
    public class SysUserRepository : BaseRepository<SysUser>, ISysUserRepository
    {
        public SysUserRepository(ISqlSugarClient _client) : base(_client)
        {
        }

        public IEnumerable<SysUser> GetListPage(string key, int pageIndex, int pageSize, ref int total)
        {
            return client.Queryable<SysUser>().ToPageList(pageIndex, pageSize, ref total);
        }
    }
}
