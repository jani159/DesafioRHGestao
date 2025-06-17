using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PedidoCompra.Domain.Entities;
using PedidoCompra.Domain.Interfaces;
using PedidoCompra.Infrastructure.Data;

namespace PedidoCompra.Infrastructure.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(PedidoCompraContext context) : base(context){ }

        public async Task<Cliente?> ObterClientePorIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }
        public async Task<Cliente> AtualizarClienteAsync(Cliente cliente)
        {
            await UpdateAsync(cliente);
            var clienteAtualizado =  await GetByIdAsync(cliente.Id);

            if (clienteAtualizado == null)
            {
                throw new Exception("Erro ao atualizar cliente.");
            }

            return clienteAtualizado;
        }
        public async Task<Cliente> IncluirClienteAsync(Cliente cliente)
        {
            await AddAsync(cliente);
            var clienteIncluido = await GetByIdAsync(cliente.Id);

            if (clienteIncluido == null)
            {
                throw new Exception("Erro ao incluir cliente.");
            }

            return clienteIncluido;

        }
        public async Task<IEnumerable<Cliente>> ListarTodosClientesAsync()
        {
            return await GetAllAsync();
        }
        public async Task<bool> RemoverClienteAsync(int id)
        {
            var cliente = await GetByIdAsync(id);
            if (cliente == null)
            {
                return false;
            }
            await DeleteAsync(id);
            return true;

        }
    }
}
