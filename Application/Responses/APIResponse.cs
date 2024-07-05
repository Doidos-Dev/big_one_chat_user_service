using System.Text.Json.Serialization;

namespace Application.Responses
{
    public class APIResponse<T>
    {
        public string Microservice { get; private set; } = "SERVICE_USER";
        public string? Operation { get; private set; }

        [JsonPropertyName("code_response")]
        public int CodeResponse { get; private set; }

        [JsonPropertyName("is_operaction_success")]
        public bool IsOperationSuccess { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("result_message")]
        public string? Message { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("response")]
        public T? Response { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("response_list")]
        public IEnumerable<T>? ResponseList { get; private set; }

        public APIResponse(
            string? operation,
            int codeResponse,
            bool isOperationSuccess,
            string? message,
            T? response,
            IEnumerable<T>? responseList)
        {
            Operation = operation;
            CodeResponse = codeResponse;
            IsOperationSuccess = isOperationSuccess;
            Message = message;
            Response = response;
            ResponseList = responseList;
        }

        public APIResponse(
            string? operation,
            int codeResponse,
            bool isOperationSuccess,
            string? message)
        {
            Operation = operation;
            CodeResponse = codeResponse;
            IsOperationSuccess = isOperationSuccess;
            Message = message;
        }
    }
}
