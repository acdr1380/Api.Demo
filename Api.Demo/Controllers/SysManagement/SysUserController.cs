using Api.Common;
using Api.IService.SysManagement;
using Api.Model.SysManagement;
using Api.Service.SysManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlSugar;

namespace Api.Demo.Controllers.SysManagement
{
    [Route("system/user")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private ISysUserService _service;
        public SysUserController(ISysUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResponse> Get()
        {
            await Task.Run(() => Thread.Sleep(2000));
            return new ActionResponse()
            {
                Data = await _service.GetList(),
            };
        }

        //[HttpGet("{id:int}")] 可以设置路由匹配参数的类型，类型不符，不会去匹配
        [HttpGet("{id}")]
        public async Task<ActionResponse> Get(string id)
        {
            return new ActionResponse()
            {
                Data = await _service.Get(id),
            };
        }

        [HttpPost]
        public async Task<ActionResponse> Post([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            await _service.Add(new List<SysUser>() { d });
            return new ActionResponse()
            {
                Data = d,
                Message = "添加成功！"
            };
        }

        [HttpPost("login")]
        public async Task<ActionResponse> Login([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            if (d.UserAccount == null || d.PassWord == null)
                throw new Exception("账号跟密码不能为空！");
            var user = await _service.Login(d.UserAccount, d.PassWord);
            return new ActionResponse()
            {
                Data = user,
                Message = "登录成功！"
            };
        }

        [HttpPut]
        public async Task<ActionResponse> Put([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            await _service.Update(new List<SysUser>() { d });
            return new ActionResponse()
            {
                Data = d,
                Message = "更新成功！"
            };
        }

        [HttpDelete]
        public async Task<ActionResponse> Delete(string id)
        {
            await _service.Delete(new List<string>() { id });
            return new ActionResponse()
            {
                Message = "删除成功！"
            };
        }
    }
}
