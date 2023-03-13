using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
