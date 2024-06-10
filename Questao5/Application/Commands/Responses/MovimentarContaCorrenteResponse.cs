namespace Questao5.Application.Commands.Responses
{
    public class MovimentarContaCorrenteResponse
    {
        public MovimentarContaCorrenteResponse(string idMovimento)
        {
            IdMovimento = idMovimento;
        }

        /// <summary>
        ///     ID da Movimentação gerada.
        /// </summary>
        /// <example>b5f91b29-e895-45df-931a-6205055e34c2</example>
        public string IdMovimento { get; set; } = default!;
    }
}
