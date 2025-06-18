using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; } // Chave estrangeira para Cliente
        public string Descricao { get; set; } = string.Empty;
        private DateTime _dataPedido = DateTime.UtcNow;
        public DateTime DataPedido 
        { 
            get => _dataPedido;
            set => _dataPedido = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        // Relacionamento com Cliente
        public Cliente Cliente { get; set; }
        // Relacionamento com Itens do Pedido
        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        // Propriedade para calcular o valor total do pedido
        public decimal ValorTotal => Itens.Sum(item => item.ValorTotal);

        // Construtor
        public Pedido() { }

        public Pedido(int id, int clienteID, string descricao)
        {
            Id = id;
            ClienteId = clienteID;
            Descricao = descricao;
            _dataPedido = DateTime.UtcNow;
        }
    }
}
