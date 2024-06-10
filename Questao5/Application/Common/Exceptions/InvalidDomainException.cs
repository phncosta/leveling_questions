using System.Net;

namespace Questao5.Application.Common.Exceptions
{
    public class InvalidDomainException : Exception
    {
        public InvalidDomainException(string errorType, string errorDescription, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ErrorType = errorType;
            ErrorDescription = errorDescription;
            StatusCode = statusCode;
        }

        public string ErrorType { get; private set; } = string.Empty;

        public string ErrorDescription { get; private set; } = string.Empty;

        public HttpStatusCode StatusCode { get; private set; }
    }
}
