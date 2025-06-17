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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ClienteDTO> AtualizarClienteAsync(int id, ClienteRequestDTO clienteDTO)
        {
            var clienteOld = await _clienteRepository.ObterClientePorIdAsync(id);

            if (Validacoes.EhNullOuVazio(clienteOld))
            {
                throw new ArgumentException("Cliente não encontrado.");
            }

            var clienteNew = _mapper.Map<ClienteRequestDTO, Cliente>(clienteDTO);

            clienteNew.Id = id;
            Validacoes.ValidarEhNullOuVazio(clienteNew.Nome, nameof(clienteNew.Nome));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Email, nameof(clienteNew.Email));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Telefone, nameof(clienteNew.Telefone));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Endereco, nameof(clienteNew.Endereco));

            var clienteAtualizado = await _clienteRepository.AtualizarClienteAsync(clienteNew);

            if (Validacoes.EhNullOuVazio(clienteAtualizado))
            {
                throw new ArgumentException("Erro ao atualizar cliente.");
            }

            var clienteDTOAtualizado = _mapper.Map<Cliente, ClienteDTO>(clienteAtualizado);
            return clienteDTOAtualizado;
        }

        public async Task<ClienteDTO> IncluirClienteAsync(ClienteRequestDTO clienteDTO)
        {
            // Verifica se o clienteDTO não é null
            if (clienteDTO == null)
            {
                throw new ArgumentNullException(nameof(clienteDTO), "O DTO do cliente não pode ser nulo.");
            }

            // Mapeia o DTO para a entidade Cliente
            var clienteNew = _mapper.Map<ClienteRequestDTO, Cliente>(clienteDTO);

            // Verifica se o clienteNew não é null após o mapeamento
            if (clienteNew == null)
            {
                throw new ArgumentException("Falha ao mapear o Cliente.");
            }

            Validacoes.ValidarEhNullOuVazio(clienteNew.Nome, nameof(clienteNew.Nome));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Email, nameof(clienteNew.Email));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Telefone, nameof(clienteNew.Telefone));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Endereco, nameof(clienteNew.Endereco));

            Console.WriteLine(clienteNew);

            var clienteIncluido = await _clienteRepository.IncluirClienteAsync(clienteNew);
            if (Validacoes.EhNullOuVazio(clienteIncluido) || clienteIncluido.Id == 0)
            {
                throw new ArgumentException("Erro ao incluir cliente.");
            }

            var clienteDTOIncluido = _mapper.Map<Cliente, ClienteDTO>(clienteIncluido);
            return clienteDTOIncluido;
        }

        public async Task<IEnumerable<ClienteDTO>> ListarTodosClientesAsync()
        {
            var clientes = await _clienteRepository.ListarTodosClientesAsync();

            if (Validacoes.EhNullOuVazio(clientes))
            {
                return new List<ClienteDTO>();
            }
            var clientesDTO = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clientes);
            return clientesDTO;
        }

        public async Task<ClienteDTO> ObterClientePorIdAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("O ID do cliente deve ser maior que zero.");
            }

            var cliente = await _clienteRepository.ObterClientePorIdAsync(id);

            if (Validacoes.EhNullOuVazio(cliente))
            {
                throw new ArgumentException("Cliente não encontrado.");
            }

            var clienteDTO = _mapper.Map<Cliente, ClienteDTO>(cliente);
            return clienteDTO;
        }

        public async Task<bool> RemoverClienteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID do cliente deve ser maior que zero.");
            }

            var cliente = await _clienteRepository.ObterClientePorIdAsync(id);

            if (Validacoes.EhNullOuVazio(cliente))
            {
                throw new ArgumentException("Cliente não encontrado.");
            }

            var removeu = await _clienteRepository.RemoverClienteAsync(id);
            if (!removeu)
            {
                throw new ArgumentException("Erro ao remover cliente.");
            }
            return removeu;
        }
    }
}
