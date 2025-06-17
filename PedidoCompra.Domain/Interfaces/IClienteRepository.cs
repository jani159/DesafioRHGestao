using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Domain.Interfaces
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<Cliente?> ObterClientePorIdAsync(int id);
        Task<Cliente> AtualizarClienteAsync(Cliente cliente);
        Task<Cliente> IncluirClienteAsync(Cliente cliente);
        Task<IEnumerable<Cliente>> ListarTodosClientesAsync();
        Task<bool> RemoverClienteAsync(int id);

    }
}
