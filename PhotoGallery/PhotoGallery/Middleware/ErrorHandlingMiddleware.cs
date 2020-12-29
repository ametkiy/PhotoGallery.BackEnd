using Microsoft.AspNetCore.Http;
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

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
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
