﻿using System.Net;
using Api.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Demo.Middleware
{
    /// <summary>
    /// 全局异常捕获
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new Response()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                });
            }
        }
    }


}
