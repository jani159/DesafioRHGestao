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
        private readonly ILogger<PedidoController> _logger;


        public PedidoController(IPedidoService pedidoService, ILogger<PedidoController> logger)
        {
            _pedidoService = pedidoService;
            _logger = logger;
        }

        [HttpGet("listarTodos")]
        public async Task<IActionResult> ListarTodosPedidos()
        {
            try
            {
                _logger.LogInformation("Iniciando a inclusão do pedido.");
                var pedidos = await _pedidoService.ListarTodosPedidosAsync();

                if (pedidos == null || !pedidos.Any())
                {
                    _logger.LogInformation("Nenhum pedido encontrado.");
                    return NotFound("Nenhum pedido encontrado.");
                }
                _logger.LogInformation($"Total de pedidos encontrados: {pedidos.Count()}");

                return Ok(pedidos);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao listar todos os pedidos.");
                return BadRequest($"Erro ao listar pedidos: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar todos os pedidos.");
                return StatusCode(500, "Erro interno ao listar pedidos.");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPedidoPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando pedido com ID: {id}.");

                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para busca de pedido.");
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);

                if (pedido == null)
                {
                    _logger.LogWarning($"Pedido com ID {id} não encontrado.");
                    return NotFound("Pedido não encontrado.");
                }

                _logger.LogInformation($"Pedido com ID {id} encontrado com sucesso.");
                return Ok(pedido);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao obter pedido por ID.");
                return BadRequest(ex.Message);  // Retorna erro 400 com a mensagem de erro detalhada
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao obter pedido por ID.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("incluir")]
        public async Task<IActionResult> IncluirPedido([FromBody] PedidoRequestDTO pedidoDTO)
        {
            try
            {
                _logger.LogInformation("Iniciando a inclusão do pedido.");

                if (pedidoDTO.ClienteId <= 0)
                {
                    _logger.LogWarning("ID do cliente inválido fornecido para inclusão de pedido.");
                    return BadRequest("ID do cliente inválido. Por favor, informe um ID válido.");
                }

                if (pedidoDTO == null)
                {
                    _logger.LogWarning("Dados do pedido inválidos fornecidos para inclusão.");
                    return BadRequest("Por favor informe os dados do pedido.");
                }

                if (pedidoDTO.Itens == null || !pedidoDTO.Itens.Any())
                {
                    _logger.LogWarning("Nenhum item de pedido fornecido para inclusão.");
                    return BadRequest("Por favor informe os itens do pedido.");
                }

                var pedidoNew = await _pedidoService.IncluirPedidoAsync(pedidoDTO);

                if (pedidoNew == null)
                {
                    _logger.LogWarning("Erro ao incluir pedido. Dados inválidos ou inconsistentes.");
                    return BadRequest("Erro ao incluir pedido. Verifique os dados e tente novamente.");
                }
                _logger.LogInformation("Pedido incluído com sucesso.");
                return Ok(pedidoNew);

            } catch(ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao incluir pedido.");
                return BadRequest($"Erro ao incluir pedido: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao incluir pedido.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarPedido([FromBody] PedidoRequestDTO pedidoDTO, [FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando a atualização do pedido com ID: {id}.");
                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para atualização de pedido.");
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                if (pedidoDTO == null)
                {
                    _logger.LogWarning("Dados do pedido inválidos fornecidos para atualização.");
                    return BadRequest("Dados inválidos para efetuar a atualização do pedido. Porfavor, verfique os dados preenchidos e tente novamente.");
                }

                var pedidoAtualizado = await _pedidoService.AtualizarPedidoAsync(id, pedidoDTO);
                if (pedidoAtualizado == null)
                {
                    _logger.LogWarning($"Pedido com ID {id} não encontrado para atualização.");
                    return NotFound("O pedido não foi encontrado.");
                }

                _logger.LogInformation($"Pedido com ID {id} atualizado com sucesso.");
                return Ok(pedidoAtualizado);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pedido.");
                return BadRequest($"Erro ao atualizar pedido: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao atualizar pedido.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverPedido(int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando a remoção do pedido com ID: {id}.");

                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para remoção de pedido.");
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var pedidoRemovido = await _pedidoService.RemoverPedidoAsync(id);
                if (!pedidoRemovido)
                {
                    _logger.LogWarning($"Pedido com ID {id} não encontrado para remoção.");
                    return NotFound("Pedido não encontrado.");
                }
                _logger.LogInformation($"Pedido com ID {id} removido com sucesso.");
                return Ok("Pedido removido com sucesso!");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao remover pedido.");
                return BadRequest($"Erro ao remover pedido: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao remover pedido.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }
    }
}
