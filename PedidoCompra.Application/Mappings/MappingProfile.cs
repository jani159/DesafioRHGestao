using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PedidoCompra.Application.DTOs;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Cliente
            CreateMap<Cliente, ClienteDTO>().ReverseMap();

            //Produto
            CreateMap<Produto, ProdutoDTO>().ReverseMap();

            //Pedido
            CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap()
                .ForMember(dest => dest.NomeProduto, opt => opt.MapFrom(src => src.Produto.Nome))
                .ForMember(dest => dest.ValorUnitario, opt => opt.MapFrom(src => src.Produto.Valor));

            CreateMap<Pedido, PedidoDTO>().ReverseMap()
                .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente.Nome));
        }
    }
}
