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
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        [HttpGet("listarTodos")]
        public async Task<IActionResult> ListarTodosProdutos()
        {
            try
            {
                _logger.LogInformation("Iniciando a listagem de todos os produtos.");
                var produtos = await _produtoService.ListarTodosProdutosAsync();
                
                if (produtos == null || !produtos.Any())
                {
                    _logger.LogInformation("Nenhum produto encontrado.");
                    return NotFound("Nenhum produto encontrado.");
                }

                _logger.LogInformation($"Total de produtos encontrados: {produtos.Count()}");
                return Ok(produtos);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao listar todos os produtos.");
                return BadRequest(ex.Message);  // Retorna erro 400 com a mensagem de erro detalhada
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar todos os produtos.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProdutoPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando produto com ID: {id}.");
                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para busca de produto.");
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var produto = await _produtoService.ObterProdutoPorIdAsync(id);

                if (produto == null)
                {
                    _logger.LogWarning($"Produto com ID {id} não encontrado.");
                    return NotFound("Produdo não encontrado.");
                }

                _logger.LogInformation($"Produto encontrado: {produto.Descricao}");
                return Ok(produto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao obter produto por ID.");
                return BadRequest(ex.Message);  // Retorna erro 400 com a mensagem de erro detalhada
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao obter produto por ID.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }

        [HttpPost("incluir")]
        public async Task<IActionResult> IncluirProduto([FromBody] ProdutoRequestDTO produtoDTO)
        {
            try
            {
                _logger.LogInformation("Iniciando a inclusão de um novo produto.");
                if (produtoDTO == null)
                {
                    _logger.LogWarning("Dados do produto não informados para inclusão.");
                    return BadRequest("Por favor informe os dados do produto.");
                }

                var produtoNew = await _produtoService.IncluirProdutoAsync(produtoDTO);
                
                if (produtoNew == null)
                {
                    _logger.LogWarning("Erro ao incluir produto. Verifique os dados e tente novamente.");
                    return BadRequest("Erro ao incluir produto. Verifique os dados e tente novamente.");
                }
                _logger.LogInformation($"Produto incluído com sucesso: {produtoNew.Descricao}");
                return Ok(produtoNew);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao incluir produto.");
                return BadRequest($"Erro ao incluir produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao incluir produto.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarProduto([FromBody] ProdutoRequestDTO produtoDTO, [FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando a atualização do produto com ID: {id}.");
                if (produtoDTO == null)
                {
                    _logger.LogWarning("Dados do produto não informados para atualização.");
                    return BadRequest("Dados inválidos para efetuar a atualização do produto. Por favor, verifique os dados preenchidos e tente novamente.");
                }
                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para atualização de produto.");
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var produtoAtualizado = await _produtoService.AtualizarProdutoAsync(id, produtoDTO);
                if (produtoAtualizado == null)
                {
                    _logger.LogWarning($"Produto com ID {id} não encontrado para atualização.");
                    return NotFound("Produdo não encontrado.");
                }

                _logger.LogInformation($"Produto com ID {id} atualizado com sucesso: {produtoAtualizado.Descricao}");
                return Ok(produtoAtualizado);

            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao atualizar produto.");
                return BadRequest($"Erro ao atualizar produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao atualizar produto.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }

            
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverProduto(int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando a remoção do produto com ID: {id}.");
                if (id <= 0)
                {
                    _logger.LogWarning("ID inválido fornecido para remoção de produto.");
                    return BadRequest("ID inválido. Por favor, informe um ID válido.");
                }

                var produtoRemovido = await _produtoService.RemoverProdutoAsync(id);
                if (!produtoRemovido)
                {
                    _logger.LogWarning($"Produto com ID {id} não encontrado para remoção.");
                    return NotFound("Erro ao remover produto.");
                }

                _logger.LogInformation($"Produto com ID {id} removido com sucesso.");
                return Ok("Produto removido com sucesso!");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao remover produto.");
                return BadRequest($"Erro ao remover produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao remover produto.");
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }

        }
    }
}
