using Application.Enums;
using System.Text.Json.Serialization;

namespace Application.Responses
{
    public class APIResponse<T>
    {
        [JsonPropertyName("microservice")]
        public string? Microservice { get; set; }

        [JsonPropertyName("code_response")]
        public CodeEnum CodeResponse { get; set; }
        [JsonPropertyName("is_operaction_success")]
        public bool IsOperationSuccess { get; set; }
        [JsonPropertyName("result_message")]
        public string? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("response")]
        public T? Response { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("response_list")]
        public IEnumerable<T>? ResponseList { get; set; }
    }
}
