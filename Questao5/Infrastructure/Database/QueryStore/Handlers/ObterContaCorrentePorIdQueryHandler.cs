using MediatR;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.Repositories.Interfaces;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers
{
    public class ObterContaCorrentePorIdQueryHandler : IRequestHandler<ObterContaCorrentePorIdRequest, ContaCorrente>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ObterContaCorrentePorIdQueryHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ContaCorrente> Handle(ObterContaCorrentePorIdRequest request, CancellationToken cancellationToken)
        {
            return await _contaCorrenteRepository.ObterContaCorrentePorIdAsync(request.IdContaCorrente);
        }
    }
}
