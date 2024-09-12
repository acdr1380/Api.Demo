using SqlSugar;
using Api.IService;
using Api.Model;
using Api.Common;
using Api.Common.Model;

namespace Api.Service
{
    /// <summary>
    /// 基类服务
    /// </summary>
    /// <typeparam name="T"></typeparam
    public abstract class BaseService<T> : IBaseService<T> where T : BaseModel, new()
    {
        // 数据库连接
        protected readonly ISqlSugarClient client;

        public BaseService(ISqlSugarClient c)
        {
            client = c;
        }

        /// <summary>
        /// 根据主键查询单个
        /// </summary>
        /// <param name="id">表格主键</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<T> Get(string id)
        {
            try
            {
                var model = await client.Queryable<T>().Where(x => x.Id == id).SingleAsync();
                return model is null ? throw new Exception("未查询到信息") : model;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页码大小</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<Page<T>> GetListPage(int pageIndex, int pageSize)
        {
            try
            {
                Page<T> page = new(pageIndex, pageSize);
                RefAsync<int> totalCount = 0, pageCount = 0;

                var model = await client.Queryable<T>().ToPageListAsync(pageIndex, pageSize, totalCount, pageCount);
                page.Data = model;
                page.Total = totalCount;
                return page;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误：{ex.Message}");
            }
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
                return await client.Queryable<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误：{ex.Message}");
            }
        }


        /// <summary>
        /// 添加单条
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<T> Add(T model)
        {
            try
            {
                // 判断是否有主键，没有就手动生成
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Id = CommonFuncs.GetGuid();
                }
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                await client.Ado.BeginTranAsync();
                int num = await client.Insertable(model).ExecuteCommandAsync();
                if (num == 0)
                {
                    await client.Ado.RollbackTranAsync();
                    throw new Exception("新增失败！");
                }
                await client.Ado.CommitTranAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<IEnumerable<T>> Add(IEnumerable<T> models)
        {
            try
            {

                foreach (T model in models)
                {
                    // 判断是否有主键，没有就手动生成
                    if (string.IsNullOrEmpty(model.Id))
                    {
                        model.Id = CommonFuncs.GetGuid();
                    }
                    model.CreateDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                }

                await client.Ado.BeginTranAsync();
                int num = await client.Insertable<T>(models).ExecuteCommandAsync();
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
                throw new Exception($"服务错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 删除单条
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Delete(string id)
        {
            try
            {
                await client.Ado.BeginTranAsync();
                int num = await client.Deleteable<T>().Where(x => x.Id == id).ExecuteCommandAsync();
                if (num == 0)
                {
                    await client.Ado.RollbackTranAsync();
                    throw new Exception("删除失败！");
                }
                await client.Ado.CommitTranAsync();
                return num > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键id数组</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> Delete(IEnumerable<string> ids)
        {
            try
            {
                await client.Ado.BeginTranAsync();
                int num = await client.Deleteable<T>().In(ids).ExecuteCommandAsync();
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
                throw new Exception($"服务错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 更新单条
        /// </summary>
        /// <param name="models">更新的实体对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<T> Update(T model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Id))
                {
                    throw new Exception("未获取到对象主键");
                }

                model.ModifiedDate = DateTime.Now;
                await client.Ado.BeginTranAsync();
                int num = await client.Updateable(model).ExecuteCommandAsync();
                if (num == 0)
                {
                    await client.Ado.RollbackTranAsync();
                    throw new Exception("更新失败！");
                }
                await client.Ado.CommitTranAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models">更新的实体对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<IEnumerable<T>> Update(IEnumerable<T> models)
        {
            try
            {
                foreach (var model in models)
                {
                    if (string.IsNullOrEmpty(model.Id))
                    {
                        throw new Exception("未获取到对象主键");
                    }

                    model.ModifiedDate = DateTime.Now;
                }

                await client.Ado.BeginTranAsync();
                int num = await client.Updateable<T>(models).ExecuteCommandAsync();
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
                throw new Exception($"服务错误：{ex.Message}");
            }
        }
    }
}
