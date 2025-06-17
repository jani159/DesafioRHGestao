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
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } //("Pendente", "Concluído", "Cancelado")
        // Relacionamento com Cliente
        public Cliente Cliente { get; set; }
        // Relacionamento com Itens do Pedido
        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        // Construtor
        public Pedido() { }

        public Pedido(int id, DateTime dataPedido, decimal valorTotal, string status, int clienteId)
        {
            Id = id;
            DataPedido = dataPedido;
            ValorTotal = valorTotal;
            Status = status;
            ClienteId = clienteId;
        }
    }
}
