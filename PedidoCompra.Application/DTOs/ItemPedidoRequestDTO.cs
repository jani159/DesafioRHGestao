using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Application.DTOs
{
    public class ItemPedidoRequestDTO
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public ItemPedidoRequestDTO() { }
        
        public ItemPedidoRequestDTO(int quantidade, int produtoId)
        {
            Quantidade = quantidade;
            ProdutoId = produtoId;
        }
    }
}
