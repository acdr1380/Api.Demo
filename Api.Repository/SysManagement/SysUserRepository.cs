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

        public Task<IEnumerable<SysUser>> GetListPage(string key, int pageIndex, int pageSize, out int total)
        {
            return client.Queryable<SysUser>().ToPageList(pageIndex, pageSize,ref total);
        }

    }
}
