using MediatR;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.Repositories.Interfaces;

namespace Questao5.Infrastructure.Database.CommandStore.Handlers
{
    public class InserirIdempotenciaCommandHandler : IRequestHandler<InserirIdempotenciaCommandRequest>
    {
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public InserirIdempotenciaCommandHandler(IIdempotenciaRepository idempotenciaRepository)
        {
            _idempotenciaRepository = idempotenciaRepository;
        }

        public async Task<Unit> Handle(InserirIdempotenciaCommandRequest request, CancellationToken cancellationToken)
        {
            await _idempotenciaRepository.InserirIdempotenciaAsync(request.Idempotencia);
            return Unit.Value;
        }
    }
}
