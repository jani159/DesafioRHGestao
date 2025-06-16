using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeEstoque { get; set; }

        // Relacionamento com Itens do Pedido
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

        // Relacionamento com Pedidos
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        // Construtor
        public Produto() { }
        public Produto(int id, string nome, string descricao, decimal valor, int quantidadeEstoque)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            QuantidadeEstoque = quantidadeEstoque;
        }
    }
}
