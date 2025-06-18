using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;
using PedidoCompra.Domain.Interfaces;
using PedidoCompra.Infrastructure.Data;

namespace PedidoCompra.Infrastructure.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(PedidoCompraContext context) : base(context) { }

        public async Task<Produto> AtualizarProdutoAsync(Produto produto)
        {
            await UpdateAsync(produto);
            var produtoAtualizado = await GetByIdAsync(produto.Id);
            if (produtoAtualizado == null)
            {
                throw new Exception("Erro ao atualizar produto.");
            }
            return produtoAtualizado;
        }

        public async Task<Produto> IncluirProdutoAsync(Produto produto)
        {
            await AddAsync(produto);
            var produtoIncluido = await GetByIdAsync(produto.Id);
            if (produtoIncluido == null)
            {
                throw new Exception("Erro ao incluir produto.");
            }
            return produtoIncluido;
        }

        public async Task<IEnumerable<Produto>> ListarTodosProdutosAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Produto?> ObterProdutoPorIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<bool> RemoverProdutoAsync(int id)
        {
            var produto = await GetByIdAsync(id);
            if (produto == null)
            {
                return false;
            }
            await DeleteAsync(id);
            return true;
        }
    }
}
