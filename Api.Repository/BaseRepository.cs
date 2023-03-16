using Api.Common;
using Api.IRepository;
using SqlSugar;

namespace Api.Repository
{
    /// <summary>
    /// 基类仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [AutoInject(typeof(IBaseRepository<>), InjectType.Scope)]
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        public readonly ISqlSugarClient client;
        public BaseRepository(ISqlSugarClient _client)
        {
            client = _client;
        }

        /// <summary>
        /// 根据主键查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<T> Get(string id)
        {
            return await Task.Run(() => client.Queryable<T>().InSingle(id));
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<IEnumerable<T>> GetList()
        {
            try
            {
                return await Task.Run(() => client.Queryable<T>().ToList());
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误:{ex.Message}");
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Add(IEnumerable<T> models)
        {
            try
            {
                await client.Ado.BeginTranAsync();
                int num = await Task.Run(() => client.Insertable<T>(models).ExecuteCommand());
                if (num == 0)
                {
                    await client.Ado.RollbackTranAsync();
                    throw new Exception("新增失败！");
                }
                await client.Ado.CommitTranAsync();
                return num > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误:{ex.Message}");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">主键id数组</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Delete(IEnumerable<string> ids)
        {
            try
            {
                await client.Ado.BeginTranAsync();
                int num = await Task.Run(() => client.Deleteable<T>().In(ids).ExecuteCommand());
                if (num == 0)
                {
                    await client.Ado.RollbackTranAsync();
                    throw new Exception("新增失败！");
                }
                await client.Ado.CommitTranAsync();
                return num > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误:{ex.Message}");
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="models">更新的实体对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<IEnumerable<T>> Update(IEnumerable<T> models)
        {
            try
            {
                await client.Ado.BeginTranAsync();
                int num = await Task.Run(() => client.Updateable<T>(models).ExecuteCommand());
                if (num == 0)
                {
                    await client.Ado.RollbackTranAsync();
                    throw new Exception("新增失败！");
                }
                await client.Ado.CommitTranAsync();
                return models;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误:{ex.Message}");
            }
        }
    }
}
