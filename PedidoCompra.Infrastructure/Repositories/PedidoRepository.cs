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
        public PedidoRepository(PedidoCompraContext context) : base(context)
        {
        }

        public async Task<Pedido?> ObterPedidoAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
