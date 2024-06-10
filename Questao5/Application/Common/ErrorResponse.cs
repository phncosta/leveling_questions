using System.Text.Json.Serialization;

namespace Questao5.Application.Common
{
    public class ErrorResponse : ApiResponse
    {
        public ErrorResponse(string errorType, string errorDescription)
        {
            ErrorType = errorType;
            ErrorDescription = errorDescription;
            Succeeded = false;
        }

        /// <summary>
        ///     Tipo do erro.
        /// </summary>
        /// <example>INVALID_ACCOUNT</example>
        [JsonPropertyName("errorType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ErrorType { get; private set; } = string.Empty;

        /// <summary>
        ///     Mensagem descritiva do erro.
        /// </summary>
        /// <example>Conta não cadastrada/localizada.</example>
        [JsonPropertyName("errorMessage")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ErrorDescription { get; private set; } = string.Empty;
    }
}
