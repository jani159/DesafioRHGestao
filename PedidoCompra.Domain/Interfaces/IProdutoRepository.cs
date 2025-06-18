using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Domain.Interfaces
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<Produto?> ObterProdutoPorIdAsync(int id);
        Task<Produto> AtualizarProdutoAsync(Produto produto);
        Task<Produto> IncluirProdutoAsync(Produto produto);
        Task<IEnumerable<Produto>> ListarTodosProdutosAsync();
        Task<bool> RemoverProdutoAsync(int id);
    }
}
