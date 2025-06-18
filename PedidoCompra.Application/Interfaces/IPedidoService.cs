using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Application.DTOs;

namespace PedidoCompra.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTO>> ListarTodosPedidosAsync();
        Task<PedidoDTO> ObterPedidoPorIdAsync(int id);
        Task<PedidoDTO> IncluirPedidoAsync(PedidoRequestDTO pedido);
        Task<PedidoDTO> AtualizarPedidoAsync(int id, PedidoRequestDTO pedido);
        Task<bool> RemoverPedidoAsync(int id);
    }
}
