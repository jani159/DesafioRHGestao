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

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("listarTodos")]
        public async Task<IActionResult> ListarTodosClientes()
        {
            try
            {
                var clientes = await _clienteService.ListarTodosClientesAsync();

                if (clientes == null || !clientes.Any())
                {
                    return NotFound("Nenhum cliente encontrado.");
                }

                return Ok(clientes);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(int id)
        {
            try
            {
                var cliente = await _clienteService.ObterClientePorIdAsync(id);

                if (cliente == null)
                {
                    return NotFound("Cliente não encontrado.");
                }

                return Ok(cliente);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpPost("incluir")]
        public async Task<IActionResult> IncluirCliente([FromBody] ClienteRequestDTO clienteDTO)
        {
            try
            {
                if (clienteDTO == null)
                {
                    return BadRequest("Por favor informe os dados do cliente.");
                }

                var clienteNew = await _clienteService.IncluirClienteAsync(clienteDTO);
                
                if (clienteNew == null)
                {
                    return BadRequest("Erro ao incluir cliente. Verifique os dados e tente novamente.");
                }

                return Ok(clienteNew);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarCliente([FromBody] ClienteRequestDTO clienteDTO, [FromRoute] int id)
        {
            try
            {
                if (clienteDTO == null)
                {
                    return BadRequest("Dados inválidos para efetuar a atualização do cliente. Porfavor, verfique os dados preenchidos e tente novamente.");
                }

                if (id <= 0)
                {
                    return BadRequest("ID inválido, informe um valor maior que zero.");
                }

                var clienteAtualizado = await _clienteService.AtualizarClienteAsync(id, clienteDTO);
                if (clienteAtualizado == null)
                {
                    return NotFound("Cliente não encontrado.");
                }

                return Ok(clienteAtualizado);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverCliente(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido, informe um valor maior que zero.");
                }

                var clienteRemovido = await _clienteService.RemoverClienteAsync(id);
                if (!clienteRemovido)
                {
                    return NotFound("Erro ao remover cliente.");
                }
                return Ok("Cliente removido com sucesso!");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
            
        }
    }
}
