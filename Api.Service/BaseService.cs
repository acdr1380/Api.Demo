using SqlSugar;
using Api.IRepository;
using Api.Repository;
using Api.IService;

namespace Api.Service
{
    /// <summary>
    /// 基类服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IBaseService<T> where T : class, new()
    {
        private IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 根据主键查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<T> Get(string id)
        {
            var d = await _repository.Get(id);
            if (d is null)
                throw new Exception("未查询到信息");
            return d;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<IEnumerable<T>> GetList()
        {
            return await _repository.GetList();
        }


        /// <summary>
        /// 添加单条
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Add(T model)
        {
            return await _repository.Add(model);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Add(IEnumerable<T> models)
        {
            return await _repository.Add(models);
        }

        /// <summary>
        /// 删除单条
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键id数组</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Delete(IEnumerable<string> ids)
        {
            return await _repository.Delete(ids);
        }

        /// <summary>
        /// 更新单条
        /// </summary>
        /// <param name="models">更新的实体对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<T> Update(T model)
        {
            return await _repository.Update(model);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models">更新的实体对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<IEnumerable<T>> Update(IEnumerable<T> models)
        {
            return await _repository.Update(models);
        }
    }
}
