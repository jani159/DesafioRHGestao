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
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ProdutoDTO> AtualizarProdutoAsync(int id, ProdutoRequestDTO produtoDTO)
        {
            var produtoOld = await _produtoRepository.ObterProdutoPorIdAsync(id);

            if (Validacoes.EhNullOuVazio(produtoOld))
            {
                throw new ArgumentException("Produto não encontrado.");
            }

            // Atualiza as propriedades do produto existente
            produtoOld.Descricao = produtoDTO.Descricao;
            produtoOld.Valor = produtoDTO.Valor;
            produtoOld.QuantidadeEstoque = produtoDTO.QuantidadeEstoque;

            Validacoes.ValidarEhNullOuVazio(produtoOld.Descricao, nameof(produtoOld.Descricao));
            Validacoes.ValidarEhNullOuVazio(produtoOld.Valor, nameof(produtoOld.Valor));
            Validacoes.ValidarEhNullOuVazio(produtoOld.QuantidadeEstoque, nameof(produtoOld.QuantidadeEstoque));

            var produtoAtualizado = await _produtoRepository.AtualizarProdutoAsync(produtoOld);

            if (Validacoes.EhNullOuVazio(produtoAtualizado))
            {
                throw new ArgumentException("Erro ao atualizar produto.");
            }

            var produtoDTOAtualizado = _mapper.Map<Produto, ProdutoDTO>(produtoAtualizado);
            return produtoDTOAtualizado;
        }

        public async Task<ProdutoDTO> IncluirProdutoAsync(ProdutoRequestDTO produtoDTO)
        {
            // Verifica se o produtoDTO não é null
            if (produtoDTO == null)
            {
                throw new ArgumentNullException(nameof(produtoDTO), "O DTO do produto não pode ser nulo.");
            }

            // Mapeia o DTO para a entidade Produto
            var produtoNew = _mapper.Map<ProdutoRequestDTO, Produto>(produtoDTO);

            // Verifica se o produtoNew não é null após o mapeamento
            if (produtoNew == null)
            {
                throw new ArgumentException("Falha ao mapear o Produto.");
            }

            Validacoes.ValidarEhNullOuVazio(produtoNew.Descricao, nameof(produtoNew.Descricao));
            Validacoes.ValidarEhNullOuVazio(produtoNew.Valor, nameof(produtoNew.Valor));
            Validacoes.ValidarEhNullOuVazio(produtoNew.QuantidadeEstoque, nameof(produtoNew.QuantidadeEstoque));

            var produtoIncluido = await _produtoRepository.IncluirProdutoAsync(produtoNew);
            if (Validacoes.EhNullOuVazio(produtoIncluido) || produtoIncluido.Id == 0)
            {
                throw new ArgumentException("Erro ao incluir produto.");
            }

            var produtoDTOIncluido = _mapper.Map<Produto, ProdutoDTO>(produtoIncluido);
            return produtoDTOIncluido;
        }

        public async Task<IEnumerable<ProdutoDTO>> ListarTodosProdutosAsync()
        {
            var produtos = await _produtoRepository.ListarTodosProdutosAsync();

            if (Validacoes.EhNullOuVazio(produtos))
            {
                return new List<ProdutoDTO>();
            }
            var produtosDTO = _mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoDTO>>(produtos);
            return produtosDTO;
        }

        public async Task<ProdutoDTO> ObterProdutoPorIdAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("O ID do produto deve ser maior que zero.");
            }

            var produto = await _produtoRepository.ObterProdutoPorIdAsync(id);

            if (Validacoes.EhNullOuVazio(produto))
            {
                throw new ArgumentException("Produto não encontrado.");
            }

            var produtoDTO = _mapper.Map<Produto, ProdutoDTO>(produto);
            return produtoDTO;
        }

        public async Task<bool> RemoverProdutoAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID do produto deve ser maior que zero.");
            }

            var produto = await _produtoRepository.ObterProdutoPorIdAsync(id);

            if (Validacoes.EhNullOuVazio(produto))
            {
                throw new ArgumentException("Produto não encontrado.");
            }

            var removeu = await _produtoRepository.RemoverProdutoAsync(id);
            if (!removeu)
            {
                throw new ArgumentException("Erro ao remover produto.");
            }
            return removeu;
        }
    }
}
