using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Application.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPedido { get; set; }
        public List<ItemPedidoDTO> Itens { get; set; } = new List<ItemPedidoDTO>();
        public decimal ValorTotal => Itens.Sum(item => item.ValorTotal);

        [JsonIgnore]
        public Cliente Cliente { get; set; }

        public PedidoDTO()
        {
        }
        public PedidoDTO(int id, string descricao, List<ItemPedidoDTO> itens)
        {
            Id = id;
            Descricao = descricao;
            Itens = itens ?? new List<ItemPedidoDTO>();
        }
    }
}
