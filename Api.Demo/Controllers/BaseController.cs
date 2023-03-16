using Microsoft.AspNetCore.Mvc;
using SqlSugar;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Demo.Controllers
{
    public class BaseController<TService, TModel> : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly TService _service;

        public BaseController(ILogger logger, TService service)
        {
            _logger = logger;
            _service = service;
        }
    }
}
