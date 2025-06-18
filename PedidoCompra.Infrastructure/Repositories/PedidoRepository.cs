using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;
using PedidoCompra.Domain.Interfaces;
using PedidoCompra.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PedidoCompra.Infrastructure.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(PedidoCompraContext context) : base(context) { }

        public async Task<Pedido?> ObterPedidoPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> AtualizarPedidoAsync(Pedido pedido)
        {
            await UpdateAsync(pedido);
            var pedidoAtualizado = await ObterPedidoPorIdAsync(pedido.Id);
            if (pedidoAtualizado == null)
            {
                throw new Exception("Erro ao atualizar pedido.");
            }
            return pedidoAtualizado;
        }

        public async Task<Pedido> IncluirPedidoAsync(Pedido pedido)
        {
            await AddAsync(pedido);
            var pedidoIncluido = await ObterPedidoPorIdAsync(pedido.Id);
            if (pedidoIncluido == null)
            {
                throw new Exception("Erro ao incluir pedido.");
            }
            return pedidoIncluido;
        }

        public async Task<IEnumerable<Pedido>> ListarTodosPedidosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Include(p => p.Cliente)
                .ToListAsync();
        }

        public async Task<bool> RemoverPedidoAsync(int id)
        {
            var pedido = await ObterPedidoPorIdAsync(id);
            if (pedido == null)
            {
                return false;
            }
            await DeleteAsync(id);
            return true;
        }
    }
}
