using Api.Model.SysManagement;
using Api.IService.SysManagement;
using SqlSugar;

namespace Api.Service.SysManagement
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        public SysUserService(ISqlSugarClient _client) : base(_client)
        {
        }
    }
}
