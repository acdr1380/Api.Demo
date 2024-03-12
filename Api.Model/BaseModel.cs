using SqlSugar;

namespace Api.Model
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel
    {
        [SugarColumn(IsPrimaryKey = true)] // 主键
        public string Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; } 
    }
}
