using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class InserirMovimentoContaCorrenteCommandRequest : IRequest<bool>
    {
        public InserirMovimentoContaCorrenteCommandRequest(Movimento movimento)
        {
            Movimento = movimento;
        }

        public Movimento Movimento { get; private set; }
    }
}
