using MediatR;
using Questao5.Application.Common;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Language.Operators;
using Questao5.Domain.Validations;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Application.Handlers
{
    public class ObterSaldoContaCorrenteHandler : IRequestHandler<ObterSaldoContaCorrenteRequest, ResponseData<ObterSaldoContaCorrenteResponse>>
    {
        private readonly IMediator _mediator;

        public ObterSaldoContaCorrenteHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseData<ObterSaldoContaCorrenteResponse>> Handle(ObterSaldoContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = await _mediator.Send(new ObterContaCorrentePorIdRequest(request.IdContaCorrente), cancellationToken);

            DomainValidations.ValidarContaCorrente(contaCorrente);

            var saldoResponse = await _mediator.Send(new ObterSaldoContaCorrenteQueryRequest(request.IdContaCorrente), cancellationToken);

            return new ResponseData<ObterSaldoContaCorrenteResponse>(new ObterSaldoContaCorrenteResponse()
            {
                DataConsulta = TimeZoneOperators.GetCurrentDateTime(),
                NomeTitular = contaCorrente.Nome,
                NumeroConta = contaCorrente.Numero,
                Saldo = ContaCorrenteOperators.ObterValorComLimiteCasasDecimais(saldoResponse.Saldo)
            });
        }
    }
}
