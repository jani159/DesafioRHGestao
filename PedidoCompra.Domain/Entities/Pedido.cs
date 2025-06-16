using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Application.Utils;

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

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (Validacoes.EhNullOuVazio(produto))
            {
                throw new ArgumentNullException(nameof(produto));
            }

            if (quantidade <= 0)
            {
                throw new ArgumentException("A quantidade precisa ser maior que zero.");
            }

            var item = new ItemPedido(produto, quantidade);
            Itens.Add(item);
        }

        public bool ValidarPedido()
        {
            return Itens.Any(); // Pedido deve ter pelo menos um item
        }
    }
}
