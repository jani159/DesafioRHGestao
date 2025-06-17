using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Application.DTOs
{
    public class ItemPedidoDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal => Quantidade * ValorUnitario;
        public Produto Produto { get; set; }

        public ItemPedidoDTO() { }
        public ItemPedidoDTO(int id, int quantidade, decimal valorUnitario)
        {
            Id = id;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }


    }
}
