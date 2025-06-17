using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoCompra.Application.DTOs
{
    public class ItemPedidoDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal => Quantidade * ValorUnitario;

        public ItemPedidoDTO() { }
        public ItemPedidoDTO(int id, int produtoId, string nomeProduto, int quantidade, decimal valorUnitario)
        {
            Id = id;
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }


    }
}
