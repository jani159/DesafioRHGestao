using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidos.Domain.Entities;
using GestorPedidos.Domain.Interfaces;
using GestorPedidos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestorPedidos.Infrastructure.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(GestorPedidoContext context) : base(context)
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
