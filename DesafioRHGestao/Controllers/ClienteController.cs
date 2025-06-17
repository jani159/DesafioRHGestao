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

        [HttpGet]
        public async Task<IActionResult> ListarTodosClientes()
        {
            var clientes = await _clienteService.ListarTodosClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(int id)
        {
            var cliente = await _clienteService.ObterClientePorIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> IncluirCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
            {
                return BadRequest("Por favor informe os dados do cliente.");
            }
            var clienteNew = await _clienteService.IncluirClienteAsync(clienteDTO);
            return CreatedAtAction(nameof(ObterClientePorId), new { id = clienteNew.Id }, clienteNew);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            if (clienteDTO == null || clienteDTO.Id != id)
            {
                return BadRequest("Dados inválidos para efetuar a atualização do cliente. Porfavor, verfique os dados preenchidos e tente novamente.");
            }
            var clienteAtualizado = await _clienteService.AtualizarClienteAsync(clienteDTO);
            if (clienteAtualizado == null)
            {
                return NotFound();
            }

            return Ok(clienteAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCliente(int id)
        {
            var clienteRemovido = await _clienteService.RemoverClienteAsync(id);
            if (!clienteRemovido)
            {
                return NotFound("Erro ao remover cliente.");
            }
            return Ok("Cliente removido com sucesso!");
        }
    }
}
