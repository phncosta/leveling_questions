using FluentValidation;
using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Common;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentarContaCorrenteRequest : IRequest<ResponseData<MovimentarContaCorrenteResponse>>
    {
        public MovimentarContaCorrenteRequest() { }

        public MovimentarContaCorrenteRequest(string idRequisicao, string idContaCorrente, decimal valorMovimentacao, string tipoMovimento)
        {
            IdRequisicao = idRequisicao;
            IdContaCorrente = idContaCorrente;
            ValorMovimentacao = valorMovimentacao;
            TipoMovimento = tipoMovimento;
        }

        /// <summary> ID da Requisição.</summary>
        /// <example>14eeb85b-f321-42f4-a065-7543661b9e15</example>
        public string IdRequisicao { get; set; } = default!;

        /// <summary>ID da Conta Corrente.</summary>
        /// <example>K2E02051-7067-ED11-94C0-835DFA4A20L1</example>
        public string IdContaCorrente { get; set; } = default!;

        /// <summary>ID da Requisição.</summary>
        /// <example>250.33</example>
        public decimal ValorMovimentacao { get; set; } = default!;

        /// <summary>Tipo do Movimento na conta corrente: 'C' para Crédito ou 'D' para Débito.</summary>
        /// <example>C</example>
        public string TipoMovimento { get; set; } = default!;
    }

    public class MovimentarContaCorrenteRequestValidator : AbstractValidator<MovimentarContaCorrenteRequest>
    {
        public MovimentarContaCorrenteRequestValidator()
        {
            RuleFor(x => x.IdRequisicao)
                .NotEmpty()
                .WithMessage(nameof(ContaCorrenteErrorType.INVALID_VALUE));

            RuleFor(x => x.IdContaCorrente)
                .NotEmpty()
                .WithMessage(nameof(ContaCorrenteErrorType.INVALID_VALUE));

            RuleFor(x => x.TipoMovimento)
                .Must(t => Enum.TryParse<TipoMovimento>(t.ToString(), out _))
                .WithMessage(nameof(ContaCorrenteErrorType.INVALID_TYPE));

            RuleFor(x => x.ValorMovimentacao)
                .GreaterThan(0)
                .WithMessage(nameof(ContaCorrenteErrorType.INVALID_VALUE));
        }
    }
}
