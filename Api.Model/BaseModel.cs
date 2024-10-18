using SqlSugar;
using Api.Common;

namespace Api.Model
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel
    {
        [SugarColumn(IsPrimaryKey = true)] // 主键
        public string Id { get; set; } = CommonFuncs.GetGuid();
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
    }
}
