using Api.IService;
using Api.Model;
using Microsoft.AspNetCore.Mvc;


namespace Api.Demo.Controllers
{
    public class BaseController<T1,T2> : ControllerBase where T1 : BaseModel where T2 : IBaseService<T1>
    {
        protected readonly ILogger<T1> logger;

        protected readonly T2 service;

        public BaseController(ILogger<T1> l, T2 s)
        {
            logger = l;
            service = s;
        }
    }
}
