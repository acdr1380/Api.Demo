using Api.Model.SysManagement;

namespace Api.IService.SysManagement
{
    public interface ISysUserService : IBaseService<SysUser>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="key">关键字，为空就不管</param>
        /// <param name="pageIndex">页码，第几页</param>
        /// <param name="pageSize">条数，返回几条</param>
        /// <returns></returns>
        IEnumerable<SysUser> GetListPage(string key, int pageIndex, int pageSize, ref int total);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserAccount">账号</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        Task<SysUser> Login(string UserAccount, string PassWord);
    }
}
