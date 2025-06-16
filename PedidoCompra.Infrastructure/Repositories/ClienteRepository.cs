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
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(PedidoCompraContext context) : base(context)
        {
        }

    }
}
