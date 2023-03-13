
namespace Api.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <returns></returns>
        Task<T> Get(string id);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList();

        /// <summary>
        /// 添加新的数据
        /// </summary>
        /// <param name="model">新的数据对象</param>
        /// <returns></returns>
        Task<bool> Add(IEnumerable<T> models);

        /// <summary>
        /// 根据传入主键删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> Delete(IEnumerable<string> ids);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Update(IEnumerable<T> model);
    }
}

