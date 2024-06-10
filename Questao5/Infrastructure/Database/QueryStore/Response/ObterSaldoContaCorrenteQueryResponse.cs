namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class ObterSaldoContaCorrenteQueryResponse
    {
        public ObterSaldoContaCorrenteQueryResponse(decimal saldo)
        {
            Saldo = saldo;
        }

        public decimal Saldo { get; private set; }
    }
}
