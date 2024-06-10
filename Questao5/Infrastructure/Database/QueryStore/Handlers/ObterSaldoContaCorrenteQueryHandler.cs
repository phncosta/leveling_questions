using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Database.Repositories.Interfaces;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers
{
    public class ObterSaldoContaCorrenteQueryHandler : IRequestHandler<ObterSaldoContaCorrenteQueryRequest, ObterSaldoContaCorrenteQueryResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ObterSaldoContaCorrenteQueryHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ObterSaldoContaCorrenteQueryResponse> Handle(ObterSaldoContaCorrenteQueryRequest request, CancellationToken cancellationToken)
        {
            decimal saldo = await _contaCorrenteRepository.ObterSaldoAsync(request.IdContaCorrente);
            return new ObterSaldoContaCorrenteQueryResponse(saldo);
        }
    }
}
