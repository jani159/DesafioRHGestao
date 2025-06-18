using System.Collections.Generic;
using PedidoCompra.Domain.Entities;
using PedidoCompra.Domain.Validations;
using PedidoCompra.Domain.Exception;
using Xunit;

namespace PedidoCompra.Tests.Domain.Validations
{
    public class PedidoValidationTests
    {
        [Fact]
        public void ValidarPedido_ComPedidoValido_NaoDeveLancarExcecao()
        {
            var pedido = new Pedido
            {
                Descricao = "Pedido Teste",
                ClienteId = 1,
                Itens = new List<ItemPedido>
                {
                    new ItemPedido
                    {
                        ProdutoId = 1,
                        Quantidade = 2,
                        ValorTotal = 100
                    }
                }
            };

            var exception = Record.Exception(() => PedidoValidation.ValidarPedido(pedido));
            Assert.Null(exception);
        }
    }
}