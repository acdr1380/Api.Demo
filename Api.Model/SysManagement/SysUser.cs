using SqlSugar;

namespace Api.Model.SysManagement
{
    [SugarTable("sys_user")]
    public class SysUser: BaseModel
    {
        // 用户名
        public string UserName { get; set; } = string.Empty;

        // 用户账号
        public string UserAccount { get; set; } = string.Empty;

        // 用户密码
        public string PassWord { get; set; } = string.Empty;

        // 邮箱
        public string Email { get; set; } = string.Empty;

        // 机构Id
        public string OrgId { get; set; } = string.Empty;
    }
}
