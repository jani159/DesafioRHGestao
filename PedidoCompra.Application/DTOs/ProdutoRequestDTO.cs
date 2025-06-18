using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Application.DTOs
{
    public class ProdutoRequestDTO
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeEstoque{ get; set; }

        public ProdutoRequestDTO() { }
        public ProdutoRequestDTO(string descricao, decimal valor, int quantidadeEstoque)
        {
            Descricao = descricao;
            Valor = valor;
            QuantidadeEstoque = quantidadeEstoque;
        }
    }
}
