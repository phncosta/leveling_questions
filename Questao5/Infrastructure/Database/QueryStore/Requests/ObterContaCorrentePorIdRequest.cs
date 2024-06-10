using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class ObterContaCorrentePorIdRequest : IRequest<ContaCorrente>
    {
        public ObterContaCorrentePorIdRequest(string idContaCorrente)
        {
            IdContaCorrente = idContaCorrente;
        }

        public string IdContaCorrente { get; set; } = default!;
    }
}
