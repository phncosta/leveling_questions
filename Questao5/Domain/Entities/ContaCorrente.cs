namespace Questao5.Domain.Entities
{
    public class ContaCorrente : BaseEntity
    {
        public ContaCorrente() { }

        public ContaCorrente(string idContaCorrente, int numero, string nome, bool ativo, IReadOnlyCollection<Movimento>? movimentos)
        {
            IdContaCorrente = idContaCorrente;
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
            Movimentos = movimentos;
        }

        public string IdContaCorrente { get; private set; } = default!;
        public int Numero { get; private set; }
        public string Nome { get; private set; } = default!;
        public bool Ativo { get; private set; }
        public IReadOnlyCollection<Movimento>? Movimentos { get; private set; }
    }
}
