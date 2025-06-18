using Microsoft.AspNetCore.Mvc;
using PedidoCompra.Application.DTOs;
using PedidoCompra.Application.Interfaces;

namespace PedidoCompra.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("listarTodos")]
        public async Task<IActionResult> ListarTodosPedidos()
        {
            var pedidos = await _pedidoService.ListarTodosPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPedidoPorId(int id)
        {
            var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpPost("incluir")]
        public async Task<IActionResult> IncluirPedido([FromBody] PedidoRequestDTO pedidoDTO)
        {
            if (pedidoDTO == null)
            {
                return BadRequest("Por favor informe os dados do pedido.");
            }
            var pedidoNew = await _pedidoService.IncluirPedidoAsync(pedidoDTO);
            return CreatedAtAction(nameof(ObterPedidoPorId), new { id = pedidoNew.Id }, pedidoNew);
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarPedido([FromBody] PedidoRequestDTO pedidoDTO, [FromRoute] int id)
        {
            if (pedidoDTO == null)
            {
                return BadRequest("Dados inválidos para efetuar a atualização do pedido. Porfavor, verfique os dados preenchidos e tente novamente.");
            }

            var pedidoAtualizado = await _pedidoService.AtualizarPedidoAsync(id, pedidoDTO);
            if (pedidoAtualizado == null)
            {
                return NotFound();
            }

            return Ok(pedidoAtualizado);
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverPedido(int id)
        {
            var pedidoRemovido = await _pedidoService.RemoverPedidoAsync(id);
            if (!pedidoRemovido)
            {
                return NotFound("Erro ao remover pedido.");
            }
            return Ok("Pedido removido com sucesso!");
        }
    }
}
