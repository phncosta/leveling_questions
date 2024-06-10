using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repositories.Interfaces
{
    public interface IIdempotenciaRepository
    {
        Task<Idempotencia> ObterIdempotenciaPorChaveAsync(string chaveIdempotencia);
        Task InserirIdempotenciaAsync(Idempotencia idempotencia);
    }
}
