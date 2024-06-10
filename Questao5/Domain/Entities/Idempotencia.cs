namespace Questao5.Domain.Entities
{
    public class Idempotencia : BaseEntity
    {
        public Idempotencia() { }

        public Idempotencia(string chave, string dadosRequisicao, string dadosResultado)
        {
            ChaveIdempotencia = chave;
            Requisicao = dadosRequisicao;
            Resultado = dadosResultado;
        }

        public string ChaveIdempotencia { get; private set; } = default!;
        public string Requisicao { get; private set; } = default!;
        public string Resultado { get; private set; } = default!;
    }
}
