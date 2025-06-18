using PedidoCompra.Domain.Entities;
using PedidoCompra.Domain.Exception;

namespace PedidoCompra.Domain.Validations
{
    public static class PedidoValidation
    {
        public static void ValidarPedido(Pedido pedido)
        {
            if (pedido == null)
                throw new DomainException("Pedido não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(pedido.Descricao))
                throw new DomainException("Descrição do pedido é obrigatória.");

            if (pedido.ClienteId <= 0)
                throw new DomainException("ClienteId inválido.");

            if (pedido.Itens == null || !pedido.Itens.Any())
                throw new DomainException("O pedido deve conter ao menos um item.");

            foreach (var item in pedido.Itens)
            {
                if (item.Quantidade <= 0)
                    throw new DomainException("Quantidade do item deve ser maior que zero.");
                if (item.ProdutoId <= 0)
                    throw new DomainException("ProdutoId do item inválido.");
                if (item.ValorTotal < 0)
                    throw new DomainException("Valor total do item não pode ser negativo.");
            }
        }
    }
}