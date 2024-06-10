using Questao5.Application.Common;
using Questao5.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace Questao5.Infrastructure.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ErrorResponse errResponse;

            switch (exception)
            {
                case InvalidDomainException ex:
                    errResponse = new ErrorResponse(ex.ErrorType, ex.ErrorDescription);
                    response.StatusCode = (int)ex.StatusCode;
                    _logger.LogInformation($"{ex.ErrorType} - {ex.ErrorDescription}", ex);
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errResponse = new ErrorResponse("APPLICATION_EXCEPTION", exception.Message);
                    _logger.LogError(exception, exception.Message);
                    break;
            }
            var result = JsonSerializer.Serialize(errResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
