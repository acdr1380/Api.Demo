using Api.IService.SysManagement;
using Api.Model.SysManagement;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace Api.Service.SysManagement
{
    public class SysMenuService : BaseService<SysMenu>, ISysMenuService
    {
        public readonly IConfiguration configuration;
        public SysMenuService(ISqlSugarClient client, IConfiguration config) : base(client)
        {
            this.configuration = config;
        }
    }
}
