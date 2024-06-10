using Questao5.Infrastructure.Database.Repositories.Interfaces;

namespace Questao5.Infrastructure.Database.Repositories.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
            services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            return services;
        }
    }
}
