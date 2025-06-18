using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeEstoque { get; set; }

        [JsonIgnore]
        public List<ItemPedido> ItensPedido { get; set; }

        public ProdutoDTO() { }

        public ProdutoDTO(int id, string nome, string descricao, decimal valor, int quantidadeEstoque)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            QuantidadeEstoque = quantidadeEstoque;
        }
    }
}
