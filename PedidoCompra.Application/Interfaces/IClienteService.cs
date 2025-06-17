using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Application.DTOs;

namespace PedidoCompra.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> ListarTodosClientesAsync();
        Task<ClienteDTO> ObterClientePorIdAsync(int id);
        Task<ClienteDTO> IncluirClienteAsync(ClienteDTO cliente);
        Task<ClienteDTO> AtualizarClienteAsync(ClienteDTO cliente);
        Task<bool> RemoverClienteAsync(int id);
    }
}
