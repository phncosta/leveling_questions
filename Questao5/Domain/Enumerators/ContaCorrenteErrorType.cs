using System.ComponentModel;

namespace Questao5.Domain.Enumerators
{
    public enum ContaCorrenteErrorType
    {
        [Description("Conta não cadastrada/localizada.")]
        INVALID_ACCOUNT,

        [Description("Conta inativa.")]
        INACTIVE_ACCOUNT,

        [Description("Valores de input inválidos ou não positivos recebidos.")]
        INVALID_VALUE,

        [Description("Apenas o tipo Crédito (C) ou Débito (D) pode ser aceito.")]
        INVALID_TYPE
    }
}
