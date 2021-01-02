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
                _logger.LogError(
                   $"Request {context.Request?.Method} {context.Request?.Path.Value} => {context.Response?.StatusCode}, {Environment.NewLine} {ex.Message}");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError; 

            if (exception is FieldIsEmptyException || exception is FileSizeException) code = HttpStatusCode.BadRequest;
            else
            {
                if (exception is UnsupportedFileFormatException) code = HttpStatusCode.UnsupportedMediaType;
            }

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
