using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedidoCompra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaIndiceItemPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "ItensPedido");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId_ProdutoId",
                table: "ItensPedido",
                columns: new[] { "PedidoId", "ProdutoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItensPedido_PedidoId_ProdutoId",
                table: "ItensPedido");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId");
        }
    }
}
