using Microsoft.AspNetCore.Mvc;
using PedidoCompra.Application.DTOs;
using PedidoCompra.Application.Interfaces;

namespace PedidoCompra.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("listarTodos")]
        public async Task<IActionResult> ListarTodosProdutos()
        {
            try
            {
                var produtos = await _produtoService.ListarTodosProdutosAsync();
                
                if (produtos == null || !produtos.Any())
                {
                    return NotFound("Nenhum produto encontrado.");
                }

                return Ok(produtos);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProdutoPorId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var produto = await _produtoService.ObterProdutoPorIdAsync(id);

                if (produto == null)
                {
                    return NotFound("Produdo não encontrado.");
                }

                return Ok(produto);
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
        public async Task<IActionResult> IncluirProduto([FromBody] ProdutoRequestDTO produtoDTO)
        {
            try
            {
                if (produtoDTO == null)
                {
                    return BadRequest("Por favor informe os dados do produto.");
                }

                var produtoNew = await _produtoService.IncluirProdutoAsync(produtoDTO);
                return Ok(produtoNew);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro ao incluir produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarProduto([FromBody] ProdutoRequestDTO produtoDTO, [FromRoute] int id)
        {
            try
            {
                if (produtoDTO == null)
                {
                    return BadRequest("Dados inválidos para efetuar a atualização do produto. Por favor, verifique os dados preenchidos e tente novamente.");
                }
                if (id <= 0)
                {
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var produtoAtualizado = await _produtoService.AtualizarProdutoAsync(id, produtoDTO);
                if (produtoAtualizado == null)
                {
                    return NotFound("Produdo não encontrado.");
                }

                return Ok(produtoAtualizado);

            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro ao atualizar produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }

            
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverProduto(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var produtoRemovido = await _produtoService.RemoverProdutoAsync(id);
                if (!produtoRemovido)
                {
                    return NotFound("Erro ao remover produto.");
                }

                return Ok("Produto removido com sucesso!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro ao remover produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }

        }
    }
}
