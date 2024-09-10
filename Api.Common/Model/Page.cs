
namespace Api.Common.Model
{
    public class Page<T>
    {
        // 当前页码
        public int PageIndex { get; set; } = 0;
        // 页码大小
        public int PageSize { get; set; } = 0;
        // 返回数据
        public List<T>? Data { get; set; } = new List<T>();
        // 总页数
        public int TotalPages { get; set; } = 0;
        // 总条数
        public int Total { get; set; } = 0;

        public Page(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }
    }
}
