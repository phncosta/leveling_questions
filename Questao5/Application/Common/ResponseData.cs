using System.Text.Json.Serialization;

namespace Questao5.Application.Common
{
    public class ResponseData<T> : ApiResponse where T : class
    {
        public ResponseData(T? data)
        {
            Data = data;
            Succeeded = true;
        }

        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }
    }
}
