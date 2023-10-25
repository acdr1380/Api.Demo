using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Serialization;

namespace Api.Common
{
    /// <summary>
    /// 统一返回格式
    /// </summary>
    public class ActionResponse
    {
        [JsonPropertyName("Code")]
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
        [JsonPropertyName("Success")]
        public bool Success { get; set; } = true;
        [JsonPropertyName("Data")]
        public object? Data { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; } = null;
    }
}
