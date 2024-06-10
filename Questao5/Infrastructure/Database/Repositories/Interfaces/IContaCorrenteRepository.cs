using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repositories.Interfaces
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> ObterContaCorrentePorIdAsync(string idContaCorrente);
        Task<decimal> ObterSaldoAsync(string idContaCorrente);
    }
}
