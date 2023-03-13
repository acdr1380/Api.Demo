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
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        public readonly ISqlSugarClient client;

        public IBaseRepository<T> repository;

        public BaseService(ISqlSugarClient _client)
        {
            client = _client;
            repository = new BaseRepository<T>(client);
        }

        /// <summary>
        /// 根据主键查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<T> Get(string id)
        {
            return await repository.Get(id);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<IEnumerable<T>> GetList()
        {
            return await repository.GetList();
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Add(IEnumerable<T> models)
        {
            return await repository.Add(models);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">主键id数组</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Delete(IEnumerable<string> ids)
        {
            return await repository.Delete(ids);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="models">更新的实体对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<IEnumerable<T>> Update(IEnumerable<T> models)
        {
            return await repository.Update(models);
        }
    }
}
