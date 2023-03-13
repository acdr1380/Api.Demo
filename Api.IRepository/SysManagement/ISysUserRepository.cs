﻿
using Api.Model.SysManagement;
namespace Api.IRepository.SysManagement
{
    public interface ISysUserRepository : IBaseRepository<SysUser>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="key">关键字，为空就不管</param>
        /// <param name="pageIndex">页码，第几页</param>
        /// <param name="pageSize">条数，返回几条</param>
        /// <returns></returns>
        Task<IEnumerable<SysUser>> GetListPage(string key, int pageIndex, int pageSize, out int total);
    }
}
