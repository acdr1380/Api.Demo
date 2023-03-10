using Microsoft.AspNetCore.Mvc;
using Api.Model;
using Api.Demo.Common;
using Api.Service;
using SqlSugar;

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
        public async Task<ApiResponse<TestModel>> Get()
        {
            return new ApiResponse<TestModel>()
            {
                Success = true,
                Message = null,
                Data = new TestModel() { Id = "dddddd" }
            };
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<TestModel>> Get(string id)
        {
            var user = service.GetUser(id);
            if (user is null)
                throw new Exception("未找到！");
            return new ApiResponse<TestModel>()
            {
                Success = true,
                Message = null,
                Data = user
            };
        }
    }
}
