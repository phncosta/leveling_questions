using Microsoft.Extensions.DependencyInjection;
using Questao2.External.Http;
using Questao2.Interfaces;

namespace Questao2.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddComunicacaoExterna(this IServiceCollection services)
        {
            services.AddScoped<IApiCommunicationService, ApiCommunicationService>();
            return services;
        }
    }
}
