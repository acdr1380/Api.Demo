using Api.Common;
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


        [HttpGet]
        public async Task<Response> Get()
        {
            await Task.Run(() => Task.Delay(2000));
            return new Response()
            {
                Data = await service.GetList(),
            };
        }

        //[HttpGet("{id:int}")] 可以设置路由匹配参数的类型，类型不符，不会去匹配
        [HttpGet("{id}")]
        public async Task<Response> Get(string id)
        {
            return new Response()
            {
                Data = await service.Get(id),
            };
        }

        [HttpPost]
        public async Task<Response> Add([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            await service.Add(d);
            return new Response()
            {
                Data = d,
                Message = "添加成功！"
            };
        }

        [HttpPost("login")]
        public async Task<Response> Login([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            if (d.UserAccount == null || d.PassWord == null)
                throw new Exception("账号跟密码不能为空！");
            var user = await service.Login(d.UserAccount, d.PassWord);
            logger.LogError($"{user.UserName}登录了系统！");
            return new Response()
            {
                Data = user,
                Message = "登录成功！"
            };
        }

        [HttpPut]
        public async Task<Response> Update([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            await service.Update(new List<SysUser>() { d });
            return new Response()
            {
                Data = d,
                Message = "更新成功！"
            };
        }

        [HttpDelete]
        public async Task<Response> Delete(string id)
        {
            await service.Delete(new List<string>() { id });
            return new Response()
            {
                Message = "删除成功！"
            };
        }

    }
}
