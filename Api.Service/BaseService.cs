using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service
{
    /// <summary>
    /// 基类服务接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <returns></returns>
        T Get(string id);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();

        /// <summary>
        /// 添加新的数据
        /// </summary>
        /// <param name="model">新的数据对象</param>
        /// <returns></returns>
        bool Add(IEnumerable<T> models);

        /// <summary>
        /// 根据传入主键删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete(IEnumerable<string> ids);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> Update(IEnumerable<T> model);
    }


    /// <summary>
    /// 基类服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        private readonly ISqlSugarClient client;
        public BaseService(ISqlSugarClient _client)
        {
            client = _client;
        }

        /// <summary>
        /// 根据主键查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Get(string id)
        {
            return client.Queryable<T>().InSingle(id);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual IEnumerable<T> GetList()
        {
            try
            {
                return client.Queryable<T>().ToList();
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
        public virtual bool Add(IEnumerable<T> models)
        {
            try
            {
                client.Ado.BeginTran();
                int num = client.Insertable<T>(models).ExecuteCommand();
                if (num == 0)
                {
                    client.Ado.RollbackTran();
                    throw new Exception("新增失败！");
                }
                client.Ado.CommitTran();
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
        public virtual bool Delete(IEnumerable<string> ids)
        {
            try
            {
                client.Ado.BeginTran();
                int num = client.Deleteable<T>().In(ids).ExecuteCommand();
                if (num == 0)
                {
                    client.Ado.RollbackTran();
                    throw new Exception("删除失败！");
                }
                client.Ado.CommitTran();
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
        public virtual IEnumerable<T> Update(IEnumerable<T> models)
        {
            try
            {
                client.Ado.BeginTran();
                int num = client.Updateable<T>(models).ExecuteCommand();
                if (num == 0)
                {
                    client.Ado.RollbackTran();
                    throw new Exception("更新失败！");
                }
                client.Ado.CommitTran();
                return models;
            }
            catch (Exception ex)
            {
                throw new Exception($"服务错误:{ex.Message}");
            }
        }

    }
}
