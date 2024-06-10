using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Common;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("conta-corrente")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Inclui uma nova movimentação da conta corrente e retorna o ID da movimentação.
        /// </summary>
        /// <returns>ID da Movimentação registrada.</returns>
        [HttpPost("movimentacao")]
        [ProducesResponseType(typeof(ResponseData<MovimentarContaCorrenteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MovimentarContaCorrente([FromBody] MovimentarContaCorrenteRequest request)
        {
            var response = await _mediator.Send(request);

            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        ///     Recebe a identificação da conta corrente e retorna o Saldo atual da conta corrente.
        /// </summary>
        /// <param name="idConta">ID da Conta Corrente</param>
        /// <returns>Número da conta, titular, data da consulta e Saldo.</returns>
        [HttpGet("Saldo/{idConta}")]
        [ProducesResponseType(typeof(ResponseData<ObterSaldoContaCorrenteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterSaldoContaCorrente(string idConta)
        {
            var response = await _mediator.Send(new ObterSaldoContaCorrenteRequest(idConta));

            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
    }
}