using MediatR;
using Moq;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Common.Exceptions;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Language.Operators;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.UnitTests.Bootstrappers;
using Xunit;

namespace Questao5.UnitTests
{
    public class ContaCorrenteTests
    {
        [Fact(DisplayName = "Deve efetuar a consulta de saldo com sucesso")]
        [Trait("Tipo", "Consulta Saldo")]
        public async Task Handle_Returns_ValidResponse()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<ObterContaCorrentePorIdRequest>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new ContaCorrente(MockData.ID_CONTA_CORRENTE, MockData.NUM_CONTA, MockData.TITULAR, true, null));

            mediatorMock.Setup(x => x.Send(It.IsAny<ObterSaldoContaCorrenteQueryRequest>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new ObterSaldoContaCorrenteQueryResponse(MockData.SALDO));

            var handler = new ObterSaldoContaCorrenteHandler(mediatorMock.Object);

            var request = new ObterSaldoContaCorrenteRequest(MockData.ID_CONTA_CORRENTE);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(TimeZoneOperators.GetCurrentDateTime().ToString("G"), response.Data.DataConsulta.ToString("G"));
            Assert.Equal(MockData.TITULAR, response.Data.NomeTitular);
            Assert.Equal(MockData.NUM_CONTA, response.Data.NumeroConta);
            Assert.Equal(MockData.SALDO, response.Data.Saldo);
        }

        [Fact(DisplayName = "Valida se apenas contas correntes cadastradas podem consultar o saldo")]
        [Trait("Tipo", "Consulta Saldo")]
        public async Task Handle_Throws_Exception_When_ContaCorrente_NotFound()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<ObterContaCorrentePorIdRequest>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((ContaCorrente)null!);

            var handler = new ObterSaldoContaCorrenteHandler(mediatorMock.Object);

            var request = new ObterSaldoContaCorrenteRequest(MockData.ID_CONTA_CORRENTE);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDomainException>(() => handler.Handle(request, CancellationToken.None));
        }

        [Theory(DisplayName = "Valida se apenas contas correntes ativas podem consultar o saldo")]
        [InlineData(false)]
        [Trait("Tipo", "Consulta Saldo")]
        public async Task Handle_Throws_Exception_When_ContaCorrente_NotActive(bool contaAtiva)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<ObterContaCorrentePorIdRequest>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new ContaCorrente(MockData.ID_CONTA_CORRENTE, MockData.NUM_CONTA, MockData.TITULAR, contaAtiva, null));

            var handler = new ObterSaldoContaCorrenteHandler(mediatorMock.Object);

            var request = new ObterSaldoContaCorrenteRequest(MockData.ID_CONTA_CORRENTE);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDomainException>(() => handler.Handle(request, CancellationToken.None));
        }

        [Fact(DisplayName = "Deve efetuar a movimenttação da conta corrente")]
        [Trait("Tipo", "Movimentação")]
        public async Task Handle_WithValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var handler = new MovimentarContaCorrenteHandler(mediatorMock.Object);

            var request = new MovimentarContaCorrenteRequest
            {
                IdRequisicao = "1",
                IdContaCorrente = "1",
                ValorMovimentacao = 100,
                TipoMovimento = "C"
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterIdempotenciaRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Idempotencia)null!);

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterContaCorrentePorIdRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContaCorrente(MockData.ID_CONTA_CORRENTE, MockData.NUM_CONTA, MockData.TITULAR, true, null));

            mediatorMock.Setup(m => m.Send(It.IsAny<InserirMovimentoContaCorrenteCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.IdMovimento);
        }

        [Fact(DisplayName = "Apenas movimentação 'débito' ou 'crédito' podem ser aceitos")]
        [Trait("Tipo", "Movimentação")]
        public async Task Handle_InvalidTipoMovimento_ThrowsInvalidDomainException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var handler = new MovimentarContaCorrenteHandler(mediatorMock.Object);

            var request = new MovimentarContaCorrenteRequest
            {
                IdRequisicao = "1",
                IdContaCorrente = "1",
                ValorMovimentacao = 100,
                TipoMovimento = "A"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDomainException>(() => handler.Handle(request, CancellationToken.None));
        }


        [Fact(DisplayName = "Apenas valores positivos podem ser recebidos ao movimentar a conta")]
        [Trait("Tipo", "Movimentação")]
        public async Task Handle_InvalidValorMovimentacao_ThrowsInvalidDomainException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var handler = new MovimentarContaCorrenteHandler(mediatorMock.Object);

            var request = new MovimentarContaCorrenteRequest
            {
                IdRequisicao = "1",
                IdContaCorrente = "1",
                ValorMovimentacao = -300,
                TipoMovimento = "C"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDomainException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
