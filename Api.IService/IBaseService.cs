using Api.Common.Model;
using Api.Model;

namespace Api.IService
{
    /// <summary>
    /// 基类服务接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : BaseModel
    {
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <returns></returns>
        Task<T> Get(string id);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="key">关键字，为空就不管</param>
        /// <param name="pageIndex">页码，第几页</param>
        /// <param name="pageSize">条数，返回几条</param>
        /// <returns></returns>
        Task<Page<T>> GetListPage(int pageIndex, int pageSize);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList();

        /// <summary>
        /// 添加单条新的数据
        /// </summary>
        /// <param name="model">新的数据对象</param>
        /// <returns></returns>
        Task<T> Add(T model);

        /// <summary>
        /// 批量添加新的数据
        /// </summary>
        /// <param name="models">新的数据对象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> Add(IEnumerable<T> models);

        /// <summary>
        /// 根据传入主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);

        /// <summary>
        /// 根据传入主键批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> Delete(IEnumerable<string> ids);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T> Update(T model);

        /// <summary>
        /// 批量更新对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Update(IEnumerable<T> model);
    }
}
