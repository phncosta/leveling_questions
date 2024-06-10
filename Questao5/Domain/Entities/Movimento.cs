using Dapper;
using Questao5.Domain.Enumerators;
using System.Data;

namespace Questao5.Domain.Entities
{
    public class Movimento : BaseEntity
    {
        public Movimento() { }

        public Movimento(string idMovimento, string idContaCorrente, string dataMovimento, TipoMovimento tipoMovimento, decimal valor)
        {
            IdMovimento = idMovimento;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        public string IdMovimento { get; private set; } = default!;
        public string IdContaCorrente { get; private set; } = default!;
        public string DataMovimento { get; private set; } = default!;
        public TipoMovimento TipoMovimento { get; private set; }
        public decimal Valor { get; private set; } = 0.00m;
        public ContaCorrente? ContaCorrente { get; private set; }
    }

    public class TipoMovimentoHandler : SqlMapper.TypeHandler<TipoMovimento>
    {
        public override void SetValue(IDbDataParameter parameter, TipoMovimento value) => parameter.Value = value.ToString();
        public override TipoMovimento Parse(object value) => (TipoMovimento)Enum.Parse(typeof(TipoMovimento), (string)value);
    }
}
