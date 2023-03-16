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
        private ISysUserService service;
        public SysUserController(ILogger<SysUserController> logger, ISysUserService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<ApiResponse> Get()
        {
            return new ApiResponse()
            {
                Data = await service.GetList(),
            };
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> Get(string id)
        {
            return new ApiResponse()
            {
                Data = await service.Get(id),
            };
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            await service.Add(new List<SysUser>() { d });
            return new ApiResponse()
            {
                Data = d,
                Message = "添加成功！"
            };
        }

        [HttpPost("login")]
        public async Task<ApiResponse> Login([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            if (d.UserAccount == null || d.PassWord == null)
                throw new Exception("账号跟密码不能为空！");
            var user = await service.Login(d.UserAccount, d.PassWord);
            return new ApiResponse()
            {
                Data = user,
                Message = "登录成功！"
            };
        }

        [HttpPut]
        public async Task<ApiResponse> Put([FromBody] dynamic data)
        {
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            await service.Update(new List<SysUser>() { d });
            return new ApiResponse()
            {
                Data = d,
                Message = "更新成功！"
            };
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete(string id)
        {
            await service.Delete(new List<string>() { id });
            return new ApiResponse()
            {
                Message = "删除成功！"
            };
        }
    }
}
