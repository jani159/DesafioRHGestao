using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PedidoCompra.Application.DTOs;
using PedidoCompra.Application.Interfaces;
using PedidoCompra.Application.Utils;
using PedidoCompra.Domain.Interfaces;
using PedidoCompra.Domain.Entities;

namespace PedidoCompra.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository,IProdutoRepository produtoRepository , IMapper mapper)
        {
            _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private void CalcularValorTotalItem(ItemPedido item)
        {
            if (item.Produto != null)
            {
                item.ValorTotal = item.Quantidade * item.Produto.Valor;
            }
        }

        public async Task<PedidoDTO> AtualizarPedidoAsync(int id, PedidoRequestDTO pedidoDTO)
        {
            var pedidoOld = await _pedidoRepository.ObterPedidoPorIdAsync(id);

            if (pedidoOld == null)
            {
                throw new ArgumentException("Pedido não encontrado.");
            }

            // Validar os campos obrigatórios do pedido
            if (string.IsNullOrEmpty(pedidoDTO.Descricao))
            {
                throw new ArgumentException("Descrição do pedido é obrigatória.");
            }

            if (pedidoDTO.ClienteId <= 0)
            {
                throw new ArgumentException("ID do cliente inválido.");
            }

            if (pedidoDTO.Itens == null || pedidoDTO.Itens.Count == 0)
            {
                throw new ArgumentException("O pedido deve conter pelo menos um item.");
            }

            // Atualiza as propriedades básicas do pedido
            pedidoOld.Descricao = pedidoDTO.Descricao;
            pedidoOld.ClienteId = pedidoDTO.ClienteId;

            // Limpa os itens existentes
            pedidoOld.Itens.Clear();

            // Adiciona os novos itens
            foreach (var itemDTO in pedidoDTO.Itens)
            {
                if (itemDTO.ProdutoId <= 0 || itemDTO.Quantidade <= 0)
                {
                    throw new ArgumentException($"Item inválido: ProdutoId ou Quantidade são inválidos.");
                }

                var produto = await _produtoRepository.ObterProdutoPorIdAsync(itemDTO.ProdutoId);
                if (produto == null)
                {
                    throw new ArgumentException($"Produto com ID {itemDTO.ProdutoId} não encontrado.");
                }

                var item = new ItemPedido
                {
                    ProdutoId = itemDTO.ProdutoId,
                    Quantidade = itemDTO.Quantidade,
                    Produto = produto
                };

                CalcularValorTotalItem(item);
                pedidoOld.Itens.Add(item);
            }

            // Atualizar o pedido no banco de dados
            var pedidoAtualizado = await _pedidoRepository.AtualizarPedidoAsync(pedidoOld);

            if (pedidoAtualizado == null)
            {
                throw new ArgumentException("Erro ao atualizar o pedido.");
            }

            return _mapper.Map<Pedido, PedidoDTO>(pedidoAtualizado);
        }

        public async Task<PedidoDTO> IncluirPedidoAsync(PedidoRequestDTO pedidoDTO)
        {
            if (pedidoDTO == null)
            {
                throw new ArgumentNullException(nameof(pedidoDTO), "O DTO do pedido não pode ser nulo.");
            }

            // Validação dos campos obrigatórios
            if (string.IsNullOrEmpty(pedidoDTO.Descricao))
            {
                throw new ArgumentException("Descrição do pedido é obrigatória.");
            }

            if (pedidoDTO.ClienteId <= 0)
            {
                throw new ArgumentException("ID do cliente inválido.");
            }

            if (pedidoDTO.Itens == null || pedidoDTO.Itens.Count == 0)
            {
                throw new ArgumentException("O pedido deve conter pelo menos um item.");
            }

            // Cria uma nova instância do pedido
            var pedidoNew = new Pedido
            {
                Descricao = pedidoDTO.Descricao,
                ClienteId = pedidoDTO.ClienteId,
                DataPedido = DateTime.UtcNow
            };

            // Adiciona os itens ao pedido
            foreach (var itemDTO in pedidoDTO.Itens)
            {
                if (itemDTO.ProdutoId <= 0 || itemDTO.Quantidade <= 0)
                {
                    throw new ArgumentException($"Item inválido: ProdutoId ou Quantidade são inválidos.");
                }

                var produto = await _produtoRepository.ObterProdutoPorIdAsync(itemDTO.ProdutoId);
                if (produto == null)
                {
                    throw new ArgumentException($"Produto com ID {itemDTO.ProdutoId} não encontrado.");
                }

                var item = new ItemPedido
                {
                    Quantidade = itemDTO.Quantidade,
                    ProdutoId = itemDTO.ProdutoId,
                    Produto = produto,
                    Pedido = pedidoNew
                };

                pedidoNew.Itens.Add(item);
            }

            // Salva o pedido e seus itens no banco de dados
            var pedidoIncluido = await _pedidoRepository.IncluirPedidoAsync(pedidoNew);
            if (pedidoIncluido == null || pedidoIncluido.Id == 0)
            {
                throw new ArgumentException("Erro ao incluir pedido.");
            }

            return _mapper.Map<Pedido, PedidoDTO>(pedidoIncluido);
        }

        public async Task<IEnumerable<PedidoDTO>> ListarTodosPedidosAsync()
        {
            var pedidos = await _pedidoRepository.ListarTodosPedidosAsync();

            if (Validacoes.EhNullOuVazio(pedidos))
            {
                return new List<PedidoDTO>();
            }
            var pedidosDTO = _mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoDTO>>(pedidos);
            return pedidosDTO;
        }

        public async Task<PedidoDTO> ObterPedidoPorIdAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("O ID do pedido deve ser maior que zero.");
            }

            var pedido = await _pedidoRepository.ObterPedidoPorIdAsync(id);

            if (Validacoes.EhNullOuVazio(pedido))
            {
                throw new ArgumentException("Pedido não encontrado.");
            }

            var pedidoDTO = _mapper.Map<Pedido, PedidoDTO>(pedido);
            return pedidoDTO;
        }

        public async Task<bool> RemoverPedidoAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID do pedido deve ser maior que zero.");
            }

            var pedido = await _pedidoRepository.ObterPedidoPorIdAsync(id);

            if (Validacoes.EhNullOuVazio(pedido))
            {
                throw new ArgumentException("Pedido não encontrado.");
            }

            var removeu = await _pedidoRepository.RemoverPedidoAsync(id);
            if (!removeu)
            {
                throw new ArgumentException("Erro ao remover pedido.");
            }
            return removeu;
        }

    }
}
