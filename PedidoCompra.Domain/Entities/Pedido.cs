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
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } //("Pendente", "Concluído", "Cancelado")

        // Relacionamento com Cliente
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }

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
