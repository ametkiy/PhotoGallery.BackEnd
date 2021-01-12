using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhotoGallery.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PhotoGallery.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(
                       $"Request {context.Request?.Method} {context.Request?.Path.Value} => {context.Response?.StatusCode}," +
                       $" {Environment.NewLine} {ex.Message} {Environment.NewLine} {ex.InnerException}"
                       );
                }
            }
        }

        private static async Task<HttpContext> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int code = StatusCodes.Status500InternalServerError;

            if (exception is BaseException)
                 code = (exception as BaseException).ErrorCode;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
            return context;
        }
    }
}
