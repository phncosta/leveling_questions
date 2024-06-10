using MediatR;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.Repositories.Interfaces;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers
{
    public class ObterIdempotenciaQueryHandler : IRequestHandler<ObterIdempotenciaRequest, Idempotencia>
    {
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public ObterIdempotenciaQueryHandler(IIdempotenciaRepository idempotenciaRepository)
        {
            _idempotenciaRepository = idempotenciaRepository;
        }

        public async Task<Idempotencia> Handle(ObterIdempotenciaRequest request, CancellationToken cancellationToken)
        {
            return await _idempotenciaRepository.ObterIdempotenciaPorChaveAsync(request.ChaveIdempotencia);
        }
    }
}
