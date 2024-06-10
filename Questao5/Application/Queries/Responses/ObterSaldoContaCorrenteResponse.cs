using System.ComponentModel;

namespace Questao5.Application.Queries.Responses
{
    public class ObterSaldoContaCorrenteResponse
    {
        /// <summary>
        /// Número da conta corrente.
        /// </summary>
        /// <example>789</example>
        [Description("Número da conta corrente.")]
        public int NumeroConta { get; set; }

        /// <summary>
        /// Nome do titular da conta.
        /// </summary>
        /// <example>Kelly Amaral</example>
        [Description("Nome do titular da conta.")]
        public string NomeTitular { get; set; } = default!;

        /// <summary>
        /// Data e hora da consulta.
        /// </summary>
        /// <example>2024-06-10T01:42:33.7636108Z</example>
        [Description("Data e hora da consulta.")]
        public DateTime DataConsulta { get; set; } = default!;

        /// <summary>
        /// Saldo da conta corrente.
        /// </summary>
        /// <example>1350.15</example>
        [Description("Saldo da conta corrente.")]
        public decimal Saldo { get; set; }
    }
}
