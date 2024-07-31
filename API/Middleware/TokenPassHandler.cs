using Application.Enums;
using Application.Helper;
using System.Text.Json;

namespace API.Middleware
{
    public class TokenPassHandler
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly string _tokenPassUserService;

        public TokenPassHandler(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _tokenPassUserService = Environment.GetEnvironmentVariable("TOKEN_PASS_USER_SERVICE") ?? _configuration["TOKEN_PASS_USER_SERVICE"]!;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tokenPass = httpContext.Request.Headers["Token-Pass-User-Service"];

            if(tokenPass != _tokenPassUserService)
            {
                var response = JsonSerializer.Serialize(Message.Response<string>(CodeEnum.BAD, Operation.KEY_PASS_INVALID, false, [], null));

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                await httpContext.Response.WriteAsync(response);
            } else
            {
                await _next(httpContext);
            }
        }
    }
}
