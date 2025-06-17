using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Application.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPedido { get; set; }
        public List<ItemPedidoDTO> Itens { get; set; } = new List<ItemPedidoDTO>();
        public decimal ValorTotal => Itens.Sum(item => item.ValorTotal);

        public PedidoDTO()
        {
        }
        public PedidoDTO(int id, string nome, string descricao, DateTime dataPedido, List<ItemPedidoDTO> itens)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DataPedido = dataPedido;
            Itens = itens ?? new List<ItemPedidoDTO>();
        }
    }
}
