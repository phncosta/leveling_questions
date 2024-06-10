using MediatR;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.Repositories.Interfaces;

namespace Questao5.Infrastructure.Database.CommandStore.Handlers
{
    public class InserirMovimentoContaCorrenteCommandHandler : IRequestHandler<InserirMovimentoContaCorrenteCommandRequest, bool>
    {
        private readonly IMovimentoRepository _movimentoRepository;

        public InserirMovimentoContaCorrenteCommandHandler(IMovimentoRepository movimentoRepository)
        {
            _movimentoRepository = movimentoRepository;
        }

        public async Task<bool> Handle(InserirMovimentoContaCorrenteCommandRequest request, CancellationToken cancellationToken)
        {
            return await _movimentoRepository.InserirMovimentacaoContaCorrenteAsync(request.Movimento);
        }
    }
}
