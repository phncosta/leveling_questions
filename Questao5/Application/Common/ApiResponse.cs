using System.Text.Json.Serialization;

namespace Questao5.Application.Common
{
    public abstract class ApiResponse
    {
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }
    }
}
