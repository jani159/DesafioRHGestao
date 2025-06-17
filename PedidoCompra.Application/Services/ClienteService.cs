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
        public async Task<ClienteDTO> AtualizarClienteAsync(ClienteDTO clienteDTO)
        {
            var clienteOld = await _clienteRepository.ObterClientePorIdAsync(clienteDTO.Id);
            if (Validacoes.EhNullOuVazio(clienteOld))
            {
                throw new ArgumentException("Cliente não encontrado.");
            }

            var clienteNew = _mapper.Map<ClienteDTO, Domain.Entities.Cliente>(clienteDTO);

            if (clienteNew.Id != clienteOld.Id)
            {
                throw new ArgumentException("O ID do cliente não pode ser alterado.");
            }

            if (clienteNew.Id <= 0)
            {
                throw new ArgumentException("O ID do cliente deve ser maior que zero.");
            }
            Validacoes.ValidarEhNullOuVazio(clienteNew.Nome, nameof(clienteNew.Nome));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Email, nameof(clienteNew.Email));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Telefone, nameof(clienteNew.Telefone));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Endereco, nameof(clienteNew.Endereco));

            var clienteAtualizado = await _clienteRepository.AtualizarClienteAsync(clienteNew);

            if (Validacoes.EhNullOuVazio(clienteAtualizado))
            {
                throw new ArgumentException("Erro ao atualizar cliente.");
            }

            var clienteDTOAtualizado = _mapper.Map<Domain.Entities.Cliente, ClienteDTO>(clienteAtualizado);
            return clienteDTOAtualizado;
        }

        public async Task<ClienteDTO> IncluirClienteAsync(ClienteDTO clienteDTO)
        {
           var clienteNew = _mapper.Map<ClienteDTO, Domain.Entities.Cliente>(clienteDTO);
            if (!Validacoes.EhNullOuVazio(clienteNew.Id))
            {
                throw new ArgumentException("O ID do cliente não deve ser informado para inclusão.");
            }

            Validacoes.ValidarEhNullOuVazio(clienteNew.Nome, nameof(clienteNew.Nome));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Email, nameof(clienteNew.Email));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Telefone, nameof(clienteNew.Telefone));
            Validacoes.ValidarEhNullOuVazio(clienteNew.Endereco, nameof(clienteNew.Endereco));

            var clienteIncluido = await _clienteRepository.IncluirClienteAsync(clienteNew);
            if (Validacoes.EhNullOuVazio(clienteIncluido))
            {
                throw new ArgumentException("Erro ao incluir cliente.");
            }

            var clienteDTOIncluido = _mapper.Map<Domain.Entities.Cliente, ClienteDTO>(clienteIncluido);
            return clienteDTOIncluido;
        }

        public async Task<IEnumerable<ClienteDTO>> ListarTodosClientesAsync()
        {
            var clientes = await _clienteRepository.ListarTodosClientesAsync();

            if (Validacoes.EhNullOuVazio(clientes))
            {
                return new List<ClienteDTO>();
            }
            var clientesDTO = _mapper.Map<IEnumerable<Domain.Entities.Cliente>, IEnumerable<ClienteDTO>>(clientes);
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

            var clienteDTO = _mapper.Map<Domain.Entities.Cliente, ClienteDTO>(cliente);
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
