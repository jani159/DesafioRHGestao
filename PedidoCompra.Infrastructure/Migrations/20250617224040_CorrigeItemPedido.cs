using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedidoCompra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeItemPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorUnitario",
                table: "ItensPedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitario",
                table: "ItensPedido",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
