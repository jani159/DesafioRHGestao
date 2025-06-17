using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Domain.Entities
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        // Construtor
        public ItemPedido() { }
        public ItemPedido(int id, int pedidoId, int produtoId, int quantidade, decimal valorUnitario)
        {
            Id = id;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }
        public ItemPedido(Produto produto, int quantidade)
        {
            ProdutoId = produto.Id;
            Quantidade = quantidade;
            ValorUnitario = produto.Valor; // Assume que o valor unitário é o preço do produto
        }
    }
}
