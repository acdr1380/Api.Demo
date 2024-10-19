using SqlSugar;

namespace Api.Model.SysManagement
{
    [SugarTable("sys_Menus")]
    public class SysMenu : BaseModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Url { get; set; } = string.Empty;


        /// <summary>
        /// 父级id
        /// </summary>
        public string? ParentId { get; set; }
    }
}
