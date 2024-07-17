using Application.Enums;
using Application.Helper;
using Application.Responses;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(ex, httpContext);
            }
        }

        private static Task HandleException(Exception ex, HttpContext httpContext)
        {
            var response = JsonSerializer.Serialize(Message.Response<string>(CodeEnum.SERVER_ERROR, ex.Message, false, [], null));

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;

            return httpContext.Response.WriteAsync(response);
        }
    }
}
