using Microsoft.AspNetCore.Mvc;
using PedidoCompra.Application.DTOs;
using PedidoCompra.Application.Interfaces;

namespace PedidoCompra.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        [HttpGet("listarTodos")]
        public async Task<IActionResult> ListarTodosClientes()
        {
            try
            {
                _logger.LogInformation("Iniciando a listagem de todos os clientes.");
                var clientes = await _clienteService.ListarTodosClientesAsync();

                if (clientes == null || !clientes.Any())
                {
                    _logger.LogWarning("Nenhum cliente encontrado na listagem.");
                    return NotFound("Nenhum cliente encontrado.");
                }

                _logger.LogInformation($"Total de clientes encontrados: {clientes.Count()}");
                return Ok(clientes);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Erro ao listar clientes: {ex.Message}");
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro interno ao listar clientes: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando cliente com ID: {id}");
                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para busca de cliente.");
                    return BadRequest("ID inválido, informe um valor maior que zero.");
                }

                var cliente = await _clienteService.ObterClientePorIdAsync(id);

                if (cliente == null)
                {
                    _logger.LogWarning($"Cliente com ID {id} não encontrado.");
                    return NotFound("Cliente não encontrado.");
                }

                _logger.LogInformation($"Cliente encontrado: {cliente.Nome}");
                return Ok(cliente);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Erro ao obter cliente: {ex.Message}");
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro interno ao obter cliente: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpPost("incluir")]
        public async Task<IActionResult> IncluirCliente([FromBody] ClienteRequestDTO clienteDTO)
        {
            try
            {
                _logger.LogInformation("Iniciando a inclusão de um novo cliente.");
                if (clienteDTO == null)
                {
                    _logger.LogWarning("Dados do cliente não informados para inclusão.");
                    return BadRequest("Por favor informe os dados do cliente.");
                }

                var clienteNew = await _clienteService.IncluirClienteAsync(clienteDTO);
                
                if (clienteNew == null)
                {
                    _logger.LogError("Erro ao incluir cliente. Verifique os dados e tente novamente.");
                    return BadRequest("Erro ao incluir cliente. Verifique os dados e tente novamente.");
                }

                _logger.LogInformation($"Cliente incluído com sucesso: {clienteNew.Nome}");
                return Ok(clienteNew);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Erro ao incluir cliente: {ex.Message}");
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro interno ao incluir cliente: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarCliente([FromBody] ClienteRequestDTO clienteDTO, [FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando a atualização do cliente com ID: {id}.");
                if (clienteDTO == null)
                {
                    _logger.LogWarning("Dados do cliente não informados para atualização.");
                    return BadRequest("Dados inválidos para efetuar a atualização do cliente. Porfavor, verfique os dados preenchidos e tente novamente.");
                }

                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para atualização de cliente.");
                    return BadRequest("ID inválido, informe um valor maior que zero.");
                }

                var clienteAtualizado = await _clienteService.AtualizarClienteAsync(id, clienteDTO);
                if (clienteAtualizado == null)
                {
                    _logger.LogWarning($"Cliente com ID {id} não encontrado para atualização.");
                    return NotFound("Cliente não encontrado.");
                }

                _logger.LogInformation($"Cliente atualizado com sucesso: {clienteAtualizado.Nome}");
                return Ok(clienteAtualizado);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Erro ao atualizar cliente: {ex.Message}");
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro interno ao atualizar cliente: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverCliente(int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando a remoção do cliente com ID: {id}.");
                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para remoção de cliente.");
                    return BadRequest("ID inválido, informe um valor maior que zero.");
                }

                var clienteRemovido = await _clienteService.RemoverClienteAsync(id);
                if (!clienteRemovido)
                {
                    _logger.LogWarning($"Erro ao remover cliente com ID {id}. Cliente não encontrado ou já removido.");
                    return NotFound("Erro ao remover cliente.");
                }
                _logger.LogInformation($"Cliente com ID {id} removido com sucesso.");
                return Ok("Cliente removido com sucesso!");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Erro ao remover cliente: {ex.Message}");
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro interno ao remover cliente: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
            
        }
    }
}
