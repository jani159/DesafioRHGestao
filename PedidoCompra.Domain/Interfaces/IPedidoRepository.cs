using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Domain.Interfaces
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<Pedido?> ObterPedidoPorIdAsync(int id);
        Task<Pedido> AtualizarPedidoAsync(Pedido pedido);
        Task<Pedido> IncluirPedidoAsync(Pedido pedido);
        Task<IEnumerable<Pedido>> ListarTodosPedidosAsync();
        Task<bool> RemoverPedidoAsync(int id);
    }
}
