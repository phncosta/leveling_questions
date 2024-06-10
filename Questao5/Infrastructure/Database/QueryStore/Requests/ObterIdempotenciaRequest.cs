using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class ObterIdempotenciaRequest : IRequest<Idempotencia>
    {
        public ObterIdempotenciaRequest(string chaveIdempotencia)
        {
            ChaveIdempotencia = chaveIdempotencia;
        }

        public string ChaveIdempotencia { get; set; } = default!;
    }
}
