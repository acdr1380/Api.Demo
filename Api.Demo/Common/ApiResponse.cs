using System.Net;

namespace Api.Demo.Common
{
    /// <summary>
    /// 统一返回格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse
    {
        public HttpStatusCode State { get; set; } = HttpStatusCode.OK;
        public bool Success { get; set; } = true;
        public Object? Data { get; set; }
        public string? Message { get; set; } = null;
    }
}
