using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Application.DTOs
{
    public class PedidoRequestDTO
    {
        public int ClienteId { get; set; }
        public string Descricao { get; set; }
        public List<ItemPedidoRequestDTO> Itens { get; set; } = new List<ItemPedidoRequestDTO>();

        public PedidoRequestDTO()
        {
        }
        public PedidoRequestDTO(int clienteID, string descricao, List<ItemPedidoRequestDTO> itens)
        {
            ClienteId = clienteID;
            Descricao = descricao;
            Itens = itens ?? new List<ItemPedidoRequestDTO>();
        }
    }
}
