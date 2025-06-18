using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PedidoCompra.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeEstoque { get; set; }

        public List<ItemPedido> ItensPedido { get; set; }

        // Construtor
        public Produto() { }
        public Produto(int id, string descricao, decimal valor, int quantidadeEstoque)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            QuantidadeEstoque = quantidadeEstoque;
        }
    }
}
