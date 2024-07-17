using UserService.Test.Application.Enums;
using UserService.Test.Application.Responses;

namespace UserService.Test.Application.Helper
{
    public static class Message
    {
        public static APIResponse<T> Response<T>(CodeEnum codeResponse, string message, bool isOperationSuccess, IEnumerable<T> results, T? result)
        {
            return new APIResponse<T>
            {
                Microservice = "SERVICE_USER",
                CodeResponse = codeResponse,
                Message = message,
                IsOperationSuccess = isOperationSuccess,
                ResponseList = results,
                Response = result
            };
        }
    }
}
