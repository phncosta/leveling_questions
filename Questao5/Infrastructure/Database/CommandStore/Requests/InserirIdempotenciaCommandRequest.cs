using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class InserirIdempotenciaCommandRequest : IRequest
    {
        public InserirIdempotenciaCommandRequest(Idempotencia idempotencia)
        {
            Idempotencia = idempotencia;
        }

        public Idempotencia Idempotencia { get; private set; }
    }
}
