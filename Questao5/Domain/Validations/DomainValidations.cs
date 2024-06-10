using Questao5.Application.Commands.Requests;
using Questao5.Application.Common.Exceptions;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Language.Extensions;
using System.Text.Json;

namespace Questao5.Domain.Validations
{
    public class DomainValidations
    {
        public static void ValidarContaCorrente(ContaCorrente contaCorrente)
        {
            if (contaCorrente is null)
                throw new InvalidDomainException(nameof(ContaCorrenteErrorType.INVALID_ACCOUNT), ContaCorrenteErrorType.INVALID_ACCOUNT.GetDescription());

            if (!contaCorrente.Ativo)
                throw new InvalidDomainException(nameof(ContaCorrenteErrorType.INACTIVE_ACCOUNT), ContaCorrenteErrorType.INACTIVE_ACCOUNT.GetDescription());
        }

        public static void ValidarIdempotenciaMovimentoContaCorrente(MovimentarContaCorrenteRequest request, Idempotencia idempotencia)
        {
            var cachedReq = JsonSerializer.Deserialize<MovimentarContaCorrenteRequest>(idempotencia.Requisicao);

            bool isValid = cachedReq?.IdRequisicao == request.IdRequisicao
                        && cachedReq?.ValorMovimentacao == request.ValorMovimentacao
                        && cachedReq?.IdContaCorrente == request.IdContaCorrente
                        && cachedReq?.TipoMovimento == request.TipoMovimento;

            if (!isValid)
                throw new InvalidOperationException("Requisição inválida e/ou com dados inconsistentes.");
        }
    }
}
