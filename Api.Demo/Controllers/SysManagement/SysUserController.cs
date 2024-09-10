using Api.Common.Model;
using Api.IService.SysManagement;
using Api.Model.SysManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Demo.Controllers.SysManagement
{
    [Route("system/user")]
    [ApiController]
    public class SysUserController : BaseController<SysUser, ISysUserService>
    {

        public SysUserController(ILogger<SysUser> logger, ISysUserService service) : base(logger, service)
        {
        }


        [HttpPost("login")]
        public async Task<Response> Login([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            if (d.UserAccount == null || d.PassWord == null)
                throw new Exception("账号跟密码不能为空！");
            var user = await service.Login(d.UserAccount, d.PassWord);
            logger.LogError("登录了系统！");
            return new Response()
            {
                Data = user,
                Message = "登录成功！"
            };
        }

    }
}
