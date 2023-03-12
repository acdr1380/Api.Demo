using Microsoft.AspNetCore.Mvc;
using Api.Model;
using Api.Demo.Common;
using Api.Service;
using SqlSugar;
using Newtonsoft.Json;

namespace Api.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;

        private readonly TestService service;

        public TestController(ILogger<TestController> logger, ISqlSugarClient client)
        {
            _logger = logger;
            service = new TestService(client);
        }

        [HttpGet]
        public Task<ApiResponse<IEnumerable<TestModel>>> Get()
        {
            var list = service.GetList();
            return Task.FromResult(new ApiResponse<IEnumerable<TestModel>>()
            {
                Success = true,
                Message = null,
                Data = list
            });
        }

        [HttpGet("{id}")]
        public Task<ApiResponse<TestModel>> GetTestById(string id)
        {
            var user = service.GetTestModel(id);
            if (user is null)
                throw new Exception("未找到！");
            return Task.FromResult(new ApiResponse<TestModel>()
            {
                Success = true,
                Message = null,
                Data = user
            });
        }


        [HttpPost]
        public Task<ApiResponse<TestModel>> PostTest(dynamic data)
        {
            TestModel d = JsonConvert.DeserializeObject<TestModel>(JsonConvert.SerializeObject(data));
            if (d.Id is null || d.UserAccount is null || d.UserName is null || d.PassWord is null)
                throw new Exception("请检查参数，有属性为空！");

            service.Add(new List<TestModel>() { d });

            return Task.FromResult(new ApiResponse<TestModel>()
            {
                Success = true,
                Message = null,
                Data = d
            });
        }


        [HttpDelete]
        public Task<ApiResponse<TestModel>> Delete(dynamic data)
        {
            TestModel d = JsonConvert.DeserializeObject<TestModel>(JsonConvert.SerializeObject(data));
            if (d.Id is null )
                throw new Exception("请检查参数，有属性为空！");

            service.Delete(new List<string>() { d.Id });

            return Task.FromResult(new ApiResponse<TestModel>()
            {
                Success = true,
                Message = null,
                Data = d
            });
        }


        [HttpPut]
        public Task<ApiResponse<TestModel>> Update(dynamic data)
        {
            TestModel d = JsonConvert.DeserializeObject<TestModel>(JsonConvert.SerializeObject(data));
            if (d.Id is null)
                throw new Exception("请检查参数，有属性为空！");

            service.Update(new List<TestModel>() { d });

            return Task.FromResult(new ApiResponse<TestModel>()
            {
                Success = true,
                Message = null,
                Data = d
            });
        }
    }
}
