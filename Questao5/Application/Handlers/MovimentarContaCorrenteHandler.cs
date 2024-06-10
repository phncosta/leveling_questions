using FluentValidation.Results;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Common;
using Questao5.Application.Common.Exceptions;
using Questao5.Application.Mappers;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Language.Extensions;
using Questao5.Domain.Validations;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using System.Text.Json;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaCorrenteHandler : IRequestHandler<MovimentarContaCorrenteRequest, ResponseData<MovimentarContaCorrenteResponse>>
    {
        private readonly IMediator _mediator;

        public MovimentarContaCorrenteHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseData<MovimentarContaCorrenteResponse>> Handle(MovimentarContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult requestValidation = new MovimentarContaCorrenteRequestValidator().Validate(request);

            if (!requestValidation.IsValid)
            {
                foreach (var err in requestValidation.Errors)
                    if (Enum.TryParse(err.ErrorMessage, out ContaCorrenteErrorType contaCorrenteError))
                        throw new InvalidDomainException(contaCorrenteError.GetDescription(), contaCorrenteError.ToString());
            }

            var idempotencia = await _mediator.Send(new ObterIdempotenciaRequest(request.IdRequisicao), cancellationToken);

            if (idempotencia is not null && !string.IsNullOrEmpty(idempotencia.Resultado))
            {
                DomainValidations.ValidarIdempotenciaMovimentoContaCorrente(request, idempotencia);
                return JsonSerializer.Deserialize<ResponseData<MovimentarContaCorrenteResponse>>(idempotencia.Resultado)!;
            }

            ContaCorrente contaCorrente = await _mediator.Send(new ObterContaCorrentePorIdRequest(request.IdContaCorrente), cancellationToken);

            DomainValidations.ValidarContaCorrente(contaCorrente);

            var movimento = ApplicationParaDominioMapper.CriarMovimentacao(request);

            bool movimentacao = await _mediator.Send(new InserirMovimentoContaCorrenteCommandRequest(movimento), cancellationToken);

            if (!movimentacao) throw new InvalidOperationException("Não foi possível realizar a movimentação.");

            var apiResult = new ResponseData<MovimentarContaCorrenteResponse>(new MovimentarContaCorrenteResponse(movimento.IdMovimento));

            _ = _mediator.Send(new InserirIdempotenciaCommandRequest(new Idempotencia(request.IdRequisicao,
                                                                            JsonSerializer.Serialize(request),
                                                                            JsonSerializer.Serialize(apiResult)
                                                                        )), cancellationToken);

            return apiResult;
        }
    }
}
