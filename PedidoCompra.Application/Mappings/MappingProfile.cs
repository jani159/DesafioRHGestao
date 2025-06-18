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
            CreateMap<Cliente, ClienteRequestDTO>().ReverseMap();

            //Produto
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Produto, ProdutoRequestDTO>().ReverseMap()
                .ForMember(dest => dest.QuantidadeEstoque, opt => opt.MapFrom(src => src.QuantidadeEstoque > 0 ? src.QuantidadeEstoque : 0));

            //Pedido
            CreateMap<Pedido, PedidoDTO>().ReverseMap()
                .ForPath(dest => dest.Cliente.Nome, opt => opt.MapFrom(src => src.Cliente.Nome));
            CreateMap<Pedido, PedidoRequestDTO>().ReverseMap()
                .ForPath(dest => dest.Cliente.Id, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens.Select(i => new ItemPedido
                {
                    ProdutoId = i.ProdutoId,
                    Quantidade = i.Quantidade
                })));

            //ItemPedido
            CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap()
                .ForMember(dest => dest.Produto, opt => opt.Ignore())
                .ForMember(dest => dest.Pedido, opt => opt.Ignore());

            CreateMap<ItemPedido, ItemPedidoRequestDTO>().ReverseMap()
                .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.ProdutoId))
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.ValorTotal, opt => opt.Ignore()); // ValorTotal é calculado dinamicamente

            CreateMap<ItemPedidoRequestDTO, ItemPedido>()
                .ForMember(dest => dest.Produto, opt => opt.Ignore())
                .ForMember(dest => dest.Pedido, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ValorTotal, opt => opt.Ignore()); // ValorTotal é calculado dinamicamente

            CreateMap<ItemPedidoDTO, ItemPedido>()
                .ForMember(dest => dest.Produto, opt => opt.Ignore())
                .ForMember(dest => dest.Pedido, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ValorTotal, opt => opt.Ignore()); // ValorTotal é calculado dinamicamente
        }
    }
}
