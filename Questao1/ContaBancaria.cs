using System.Globalization;

namespace Questao1
{
    public class ContaBancaria
    {
        public string Titular { get; set; }
        public int Numero { get; private set; }
        public double Saldo { get; private set; } // 'double' no lugar de 'decimal' a fins de seguir a pré-implementação

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0.0;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial) : this(numero, titular)
        {
            Deposito(depositoInicial);
        }

        public void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        public void Saque(double quantia)
        {
            Saldo -= (quantia + new Rate().ValorTaxa);
        }

        public override string ToString()
        {
            return $@"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }

    internal class Rate
    {
        public Rate()
        {
            ValorTaxa = 3.5;
        }

        public double ValorTaxa { get; private set; }
    }
}
