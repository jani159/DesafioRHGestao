using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PedidoCompra.Domain.Entities
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        private decimal _valorTotal;
        public decimal ValorTotal 
        { 
            get => Produto != null ? Quantidade * Produto.Valor : _valorTotal;
            set => _valorTotal = value;
        }

        [JsonIgnore]
        public virtual Pedido? Pedido { get; set; }

        [JsonIgnore]
        public virtual Produto? Produto { get; set; }

        // Construtor
        public ItemPedido() { }
        
        public ItemPedido(int pedidoId, int produtoId, int quantidade)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public ItemPedido(Produto produto, int quantidade)
        {
            ProdutoId = produto.Id;
            Quantidade = quantidade;
            _valorTotal = quantidade * produto.Valor;
        }
    }
}
