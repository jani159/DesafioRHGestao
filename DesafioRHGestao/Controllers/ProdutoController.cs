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
            var produtos = await _produtoService.ListarTodosProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProdutoPorId(int id)
        {
            var produto = await _produtoService.ObterProdutoPorIdAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost("incluir")]
        public async Task<IActionResult> IncluirProduto([FromBody] ProdutoRequestDTO produtoDTO)
        {
            if (produtoDTO == null)
            {
                return BadRequest("Por favor informe os dados do produto.");
            }
            var produtoNew = await _produtoService.IncluirProdutoAsync(produtoDTO);
            return CreatedAtAction(nameof(ObterProdutoPorId), new { id = produtoNew.Id }, produtoNew);
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarProduto([FromBody] ProdutoRequestDTO produtoDTO, [FromRoute] int id)
        {
            if (produtoDTO == null)
            {
                return BadRequest("Dados inválidos para efetuar a atualização do produto. Porfavor, verfique os dados preenchidos e tente novamente.");
            }

            var produtoAtualizado = await _produtoService.AtualizarProdutoAsync(id, produtoDTO);
            if (produtoAtualizado == null)
            {
                return NotFound();
            }

            return Ok(produtoAtualizado);
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverProduto(int id)
        {
            var produtoRemovido = await _produtoService.RemoverProdutoAsync(id);
            if (!produtoRemovido)
            {
                return NotFound("Erro ao remover produto.");
            }
            return Ok("Produto removido com sucesso!");
        }
    }
}
