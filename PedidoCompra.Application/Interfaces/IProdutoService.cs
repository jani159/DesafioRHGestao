using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Application.DTOs;

namespace PedidoCompra.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> ListarTodosProdutosAsync();
        Task<ProdutoDTO> ObterProdutoPorIdAsync(int id);
        Task<ProdutoDTO> IncluirProdutoAsync(ProdutoRequestDTO produto);
        Task<ProdutoDTO> AtualizarProdutoAsync(int id, ProdutoRequestDTO produto);
        Task<bool> RemoverProdutoAsync(int id);
    }
}
