using SqlSugar;

namespace Api.Model.SysManagement
{
    [SugarTable("sys_user")]
    public class SysUser: BaseModel
    {
        public string UserName { get; set; }
        public string UserAccount { get; set; }
        public string PassWord { get; set; }
    }
}
