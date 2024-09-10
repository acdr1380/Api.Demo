using Api.Model.SysManagement;

namespace Api.IService.SysManagement
{
    public interface ISysUserService : IBaseService<SysUser> 
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserAccount">账号</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        Task<SysUser> Login(string UserAccount, string PassWord);
    }
}
