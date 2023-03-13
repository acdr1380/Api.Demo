

using Api.Model.SysManagement;

namespace Api.IService.SysManagement
{
    public interface ISysUserService : IBaseService<SysUser>
    {
        IEnumerable<SysUser> GetListPage(string key, int pageIndex, int pageSize, ref int total);
    }
}
