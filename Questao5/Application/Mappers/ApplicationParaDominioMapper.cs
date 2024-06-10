using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Language.Operators;

namespace Questao5.Application.Mappers
{
    public class ApplicationParaDominioMapper
    {
        public static Movimento CriarMovimentacao(MovimentarContaCorrenteRequest request)
        {
            return new Movimento(
                Guid.NewGuid().ToString(),
                request.IdContaCorrente,
                DateTime.Now.ToString("dd/MM/yyyy"),
                (TipoMovimento)Enum.Parse(typeof(TipoMovimento), request.TipoMovimento),
                ContaCorrenteOperators.ObterValorComLimiteCasasDecimais(request.ValorMovimentacao)
               );
        }
    }
}
