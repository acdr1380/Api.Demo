using Api.Common;
using Api.IService;
using Api.Model;
using Api.Model.SysManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Response> GetPageList()
        {
            // await Task.Run(() => Task.Delay(2000));
            return new Response()
            {
                Data = await service.GetList(),
            };
        }

        /// <summary>
        /// 根据Id查询字段
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>{id}可以设置路由匹配参数的类型，类型不符，不会去匹配</returns>
        [HttpGet("{id}")]
        public async Task<Response> Get(string id)
        {
            return new Response()
            {
                Data = await service.Get(id),
            };
        }

        /// <summary>
        /// 新增或者更新对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> AddOrUpdate([FromBody] dynamic data)
        {
            T1 d = JsonConvert.DeserializeObject<T1>(JsonConvert.SerializeObject(data));
            return new Response()
            {
                Data = string.IsNullOrEmpty(d.Id) ? await service.Add(d) : await service.Update(d),
                Message = "添加成功！"
            };
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Response> Update([FromBody] dynamic data)
        {
            T1 d = JsonConvert.DeserializeObject<T1>(JsonConvert.SerializeObject(data));
            return new Response()
            {
                Data = await service.Update(new List<T1>() { d }),
                Message = "更新成功！"
            };
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<Response> Delete(string id)
        {
            
            return new Response()
            {
                Data = await service.Delete(id),
                Message = "删除成功！"
            };
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<Response> Delete([FromBody] dynamic data)
        {
            List<string> ids = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(data));
            return new Response()
            {
                Data = await service.Delete(ids),
                Message = "删除成功！"
            };
        }

    }
}
