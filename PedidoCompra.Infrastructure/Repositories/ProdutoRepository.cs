using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidos.Domain.Entities;
using GestorPedidos.Domain.Interfaces;
using GestorPedidos.Infrastructure.Data;

namespace GestorPedidos.Infrastructure.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(GestorPedidoContext context) : base(context)
        {
        }
    }
}
