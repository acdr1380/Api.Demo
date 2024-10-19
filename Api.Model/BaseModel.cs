using SqlSugar;
using Api.Common;

namespace Api.Model
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; } = CommonFuncs.GetGuid();

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
    }
}
