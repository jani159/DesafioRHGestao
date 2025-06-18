using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Domain.Exception
{
    public class DomainException : IOException
    {
        public DomainException(string message) : base(message) { }
    }
}
