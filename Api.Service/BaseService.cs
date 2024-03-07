using SqlSugar;
using Api.IService;
using Api.Model;

namespace Api.Service
{
    /// <summary>
    /// 基类服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<T> Get(string id)
        {
            try
            {
                var model = await Task.Run(() => client.Queryable<T>().InSingle(id));
                return model is null ? throw new Exception("未查询到信息") : model;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误:{ex.Message}");
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
                return await Task.Run(() => client.Queryable<T>().ToList());
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误:{ex.Message}");
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
                throw new Exception($"服务错误:{ex.Message}");
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
                await client.Ado.BeginTranAsync();
                int num = await Task.Run(() => client.Insertable<T>(models).ExecuteCommand());
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
                int num = await Task.Run(() => client.Deleteable<T>().In(id).ExecuteCommand());
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
        /// 更新单条
        /// </summary>
        /// <param name="models">更新的实体对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<T> Update(T model)
        {
            try
            {
                await client.Ado.BeginTranAsync();
                int num = await Task.Run(() => client.Updateable(model).ExecuteCommand());
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
                throw new Exception($"服务错误:{ex.Message}");
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

        /// <summary>
        /// 判断泛型是否有指定属性，并且属性是否为空
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool IsPropertyNullOrEmpty(T model, string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            // 判断是否存在属性
            if (property == null)
            {
                return true;
            }

            var value = property.GetValue(model);

            // 对于引用类型（包括可空引用类型）
            if (value == null)
            {
                return true;
            }

            // 对于值类型，需根据具体类型判断其是否为默认值
            Type valueType = property.PropertyType;
            if (valueType.IsValueType && value.Equals(Activator.CreateInstance(valueType)))
            {
                return true;
            }

            return false;
        }

    }
}
