using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade{ get; set; }

        public ProdutoDTO() { }
        public ProdutoDTO(int id, string nome, string descricao, decimal valor, int quantidade)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Quantidade = quantidade;
        }
    }
}
