using SqlSugar;
using Api.IService;
using Api.Model;
using Api.Common;
using Api.Common.Model;

namespace Api.Service
{
    /// <summary>
    /// 基类服务，提供基本的 CRUD 操作
    /// </summary>
    /// <typeparam name="T">继承自 BaseModel 的模型类型</typeparam>
    public abstract class BaseService<T> : IBaseService<T> where T : BaseModel, new()
    {
        // 数据库连接客户端
        protected readonly ISqlSugarClient client;

        // 构造函数，注入数据库客户端
        public BaseService(ISqlSugarClient c)
        {
            client = c;
        }

        /// <summary>
        /// 执行带事务的方法
        /// </summary>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <param name="func">要执行的异步函数</param>
        /// <returns>操作结果</returns>
        private async Task<TResult> ExecuteWithTransaction<TResult>(Func<Task<TResult>> func)
        {
            await client.Ado.BeginTranAsync(); // 开始事务
            try
            {
                var result = await func(); // 执行传入的操作
                await client.Ado.CommitTranAsync(); // 提交事务
                return result;
            }
            catch (Exception ex)
            {
                await client.Ado.RollbackTranAsync(); // 发生异常时回滚事务
                throw new Exception($"服务错误：{ex.Message}"); // 抛出自定义异常
            }
        }

        /// <summary>
        /// 根据主键查询单个记录
        /// </summary>
        /// <param name="id">记录主键</param>
        /// <returns>返回查询到的模型</returns>
        public virtual async Task<T> Get(string id)
        {
            var model = await client.Queryable<T>().Where(x => x.Id == id).SingleAsync();
            return model ?? throw new Exception("未查询到信息"); // 如果未查询到记录，抛出异常
        }

        /// <summary>
        /// 分页查询记录
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>返回分页数据</returns>
        public virtual async Task<Page<T>> GetListPage(int pageIndex, int pageSize)
        {
            Page<T> page = new(pageIndex, pageSize);
            RefAsync<int> totalCount = 0;

            var models = await client.Queryable<T>().ToPageListAsync(pageIndex, pageSize, totalCount);
            page.Data = models; // 设置当前页的数据
            page.Total = totalCount; // 设置总记录数
            return page;
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns>返回所有记录列表</returns>
        public virtual async Task<IEnumerable<T>> GetList()
        {
            return await client.Queryable<T>().ToListAsync(); // 直接返回所有记录
        }

        /// <summary>
        /// 添加单条记录
        /// </summary>
        /// <param name="model">要添加的模型</param>
        /// <returns>返回添加后的模型</returns>
        public virtual async Task<T> Add(T model) =>
            await ExecuteWithTransaction(async () =>
            {
                model.Id ??= CommonFuncs.GetGuid(); // 如果未指定主键，则生成 GUID
                int num = await client.Insertable(model).ExecuteCommandAsync(); // 执行插入操作
                if (num == 0) throw new Exception("新增失败！"); // 插入失败抛出异常
                return model; // 返回添加后的模型
            });

        /// <summary>
        /// 批量添加记录
        /// </summary>
        /// <param name="models">要添加的模型集合</param>
        /// <returns>返回添加后的模型集合</returns>
        public virtual async Task<IEnumerable<T>> Add(IEnumerable<T> models) =>
            await ExecuteWithTransaction(async () =>
            {
                foreach (var model in models)
                {
                    model.Id ??= CommonFuncs.GetGuid(); // 生成主键
                }
                int num = await client.Insertable<T>(models).ExecuteCommandAsync(); // 执行批量插入
                if (num == 0) throw new Exception("批量新增失败！"); // 插入失败抛出异常
                return models; // 返回添加后的模型集合
            });

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <param name="id">要删除的记录主键</param>
        /// <returns>返回删除是否成功</returns>
        public virtual async Task<bool> Delete(string id) =>
            await ExecuteWithTransaction(async () =>
            {
                int num = await client.Deleteable<T>().Where(x => x.Id == id).ExecuteCommandAsync(); // 执行删除操作
                if (num == 0) throw new Exception("删除失败！"); // 删除失败抛出异常
                return num > 0; // 返回删除是否成功
            });

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="ids">要删除的记录主键集合</param>
        /// <returns>返回删除是否成功</returns>
        public virtual async Task<bool> Delete(IEnumerable<string> ids) =>
            await ExecuteWithTransaction(async () =>
            {
                int num = await client.Deleteable<T>().In(ids).ExecuteCommandAsync(); // 执行批量删除
                if (num == 0) throw new Exception("批量删除失败！"); // 删除失败抛出异常
                return num > 0; // 返回删除是否成功
            });

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <param name="model">要更新的模型</param>
        /// <returns>返回更新后的模型</returns>
        public virtual async Task<T> Update(T model) =>
            await ExecuteWithTransaction(async () =>
            {
                if (string.IsNullOrEmpty(model.Id))
                {
                    throw new Exception("未获取到对象主键"); // 确保主键存在
                }

                model.UpdatedTime = DateTime.Now; // 更新修改时间
                int num = await client.Updateable(model).ExecuteCommandAsync(); // 执行更新操作
                if (num == 0) throw new Exception("更新失败！"); // 更新失败抛出异常
                return model; // 返回更新后的模型
            });

        /// <summary>
        /// 批量更新记录
        /// </summary>
        /// <param name="models">要更新的模型集合</param>
        /// <returns>返回更新后的模型集合</returns>
        public virtual async Task<IEnumerable<T>> Update(IEnumerable<T> models) =>
            await ExecuteWithTransaction(async () =>
            {
                foreach (var model in models)
                {
                    if (string.IsNullOrEmpty(model.Id))
                    {
                        throw new Exception("未获取到对象主键"); // 确保主键存在
                    }
                    model.UpdatedTime = DateTime.Now; // 更新修改时间
                }

                int num = await client.Updateable<T>(models).ExecuteCommandAsync(); // 执行批量更新
                if (num == 0) throw new Exception("批量更新失败！"); // 更新失败抛出异常
                return models; // 返回更新后的模型集合
            });
    }
}
