using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class ObterSaldoContaCorrenteQueryRequest : IRequest<ObterSaldoContaCorrenteQueryResponse>
    {
        public ObterSaldoContaCorrenteQueryRequest(string idContaCorrente)
        {
            IdContaCorrente = idContaCorrente;
        }

        public string IdContaCorrente { get; set; } = default!;
    }
}
