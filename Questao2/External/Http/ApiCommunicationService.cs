using Flurl.Http;
using Questao2.Interfaces;

namespace Questao2.External.Http
{
    public class ApiCommunicationService : IApiCommunicationService
    {
        public async Task<HttpResponseMessage> Get(Request request)
        {
            var response = await request.Uri
                                            .WithHeaders(request.Headers)
                                            .SetQueryParams(request.Parameters)
                                            .AllowAnyHttpStatus()
                                            .GetAsync();

            return response.ResponseMessage;
        }
    }
}
