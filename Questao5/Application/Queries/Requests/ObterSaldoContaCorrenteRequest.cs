using MediatR;
using Questao5.Application.Common;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class ObterSaldoContaCorrenteRequest : IRequest<ResponseData<ObterSaldoContaCorrenteResponse>>
    {
        public ObterSaldoContaCorrenteRequest(string idContaCorrente)
        {
            IdContaCorrente = idContaCorrente;
        }

        /// <summary>ID da Conta Corrente.</summary>
        /// <example>K2E02051-7067-ED11-94C0-835DFA4A20L1</example>
        public string IdContaCorrente { get; set; } = default!;
    }
}
