using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Application.DTOs
{
    public class ItemPedidoDTO
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal => Quantidade * Produto.Valor;

        [JsonIgnore]
        public Produto Produto { get; set; }
        [JsonIgnore]
        public Pedido Pedido { get; set; }

        public ItemPedidoDTO() { }
        public ItemPedidoDTO(int id, int quantidade, int pedidoId, int produtoId)
        {
            Id = id;
            Quantidade = quantidade;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
        }


    }
}
