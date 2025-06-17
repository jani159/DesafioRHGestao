using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Application.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public ICollection<ItemPedidoDTO> ItensPedido { get; set; } = new List<ItemPedidoDTO>();

        public ICollection<PedidoDTO> Pedidos { get; set; } = new List<PedidoDTO>();

        public ClienteDTO() { }
        public ClienteDTO(int id, string nome, string email, string telefone, string endereco)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

    }
}
