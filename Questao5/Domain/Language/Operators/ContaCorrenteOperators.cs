namespace Questao5.Domain.Language.Operators
{
    public class ContaCorrenteOperators
    {
        public static decimal ObterValorComLimiteCasasDecimais(decimal valor)
        {
            return Math.Round(valor, 2, MidpointRounding.AwayFromZero) + 0.00M;
        }
    }
}
