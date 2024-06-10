using Questao2.External.Http;

namespace Questao2.Interfaces
{
    public interface IApiCommunicationService
    {
        Task<HttpResponseMessage> Get(Request request);
    }
}
