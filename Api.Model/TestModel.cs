using SqlSugar;

namespace Api.Model
{
    [SugarTable("sys_user")]
    public class TestModel
    {
        [SugarColumn(IsPrimaryKey = true)]//数据库是自增才配自增 
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserAccount { get; set; }
        public string PassWord { get; set; }
    }
}
