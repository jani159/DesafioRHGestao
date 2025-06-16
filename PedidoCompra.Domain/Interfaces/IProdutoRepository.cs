using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Domain.Interfaces
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
    }
}
