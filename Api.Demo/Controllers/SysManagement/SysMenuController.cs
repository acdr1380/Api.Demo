
using Api.IService.SysManagement;
using Api.Model.SysManagement;
using Microsoft.AspNetCore.Mvc;

namespace Api.Demo.Controllers.SysManagement
{
    [Route("system/menu")]
    [ApiController]
    public class SysMenuController : BaseController<SysMenu, ISysMenuService>
    {
        public SysMenuController(ILogger<SysMenu> l, ISysMenuService s) : base(l, s)
        {
        }
    }
}
