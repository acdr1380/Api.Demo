using Microsoft.AspNetCore.Mvc;


namespace Api.Demo.Controllers
{
    public class BaseController<T> : ControllerBase
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
