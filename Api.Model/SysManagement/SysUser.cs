using SqlSugar;

namespace Api.Model.SysManagement
{
    [SugarTable("sys_user")]
    public class SysUser: BaseModel
    {
        // 用户名
        public string UserName { get; set; }

        // 用户账号
        public string UserAccount { get; set; }

        // 用户密码
        public string PassWord { get; set; }
    }
}
