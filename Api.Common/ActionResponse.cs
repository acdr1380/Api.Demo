using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Common
{
    /// <summary>
    /// 统一返回格式
    /// </summary>
    public class ActionResponse
    {
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
        public bool Success { get; set; } = true;
        public object? Data { get; set; }
        public string? Message { get; set; } = null;
    }
}
