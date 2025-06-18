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
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);

                if (pedido == null)
                {
                    return NotFound("Pedido não encontrado.");
                }

                return Ok(pedido);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);  // Retorna erro 400 com a mensagem de erro detalhada
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("incluir")]
        public async Task<IActionResult> IncluirPedido([FromBody] PedidoRequestDTO pedidoDTO)
        {
            if (pedidoDTO == null)
            {
                return BadRequest("Por favor informe os dados do pedido.");
            }

            try
            {
                var pedidoNew = await _pedidoService.IncluirPedidoAsync(pedidoDTO);
                return Ok(pedidoNew);

            } catch(ArgumentException ex)
            {
                return BadRequest($"Erro ao incluir pedido: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarPedido([FromBody] PedidoRequestDTO pedidoDTO, [FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                if (pedidoDTO == null)
                {
                    return BadRequest("Dados inválidos para efetuar a atualização do pedido. Porfavor, verfique os dados preenchidos e tente novamente.");
                }

                var pedidoAtualizado = await _pedidoService.AtualizarPedidoAsync(id, pedidoDTO);
                if (pedidoAtualizado == null)
                {
                    return NotFound("O pedido não foi encontrado.");
                }

                return Ok(pedidoAtualizado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro ao atualizar pedido: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverPedido(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var pedidoRemovido = await _pedidoService.RemoverPedidoAsync(id);
                if (!pedidoRemovido)
                {
                    return NotFound("Pedido não encontrado.");
                }
                return Ok("Pedido removido com sucesso!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro ao remover pedido: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }
    }
}
