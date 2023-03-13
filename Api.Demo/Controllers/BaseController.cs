using Microsoft.AspNetCore.Mvc;
using SqlSugar;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Demo.Controllers
{
    public class BaseController<TService, TModel> : ControllerBase where TService : new()
    {
        private readonly ILogger<BaseController<TService, TModel>> _logger;

        private readonly TService service;

        public BaseController(ILogger<BaseController<TService, TModel>> logger, ISqlSugarClient client)
        {
            _logger = logger;
        }
    }
}
