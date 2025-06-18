using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedidoCompra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeRelacionamentoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Pedido_PedidoId1",
                table: "ItensPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Clientes_ClienteId1",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_ClienteId1",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_ItensPedido_PedidoId1",
                table: "ItensPedido");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "NomeProduto",
                table: "ItensPedido");

            migrationBuilder.DropColumn(
                name: "PedidoId1",
                table: "ItensPedido");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Pedido",
                newName: "Descricao");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Produto",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId1",
                table: "ItensPedido",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_ProdutoId1",
                table: "ItensPedido",
                column: "ProdutoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Produto_ProdutoId1",
                table: "ItensPedido",
                column: "ProdutoId1",
                principalTable: "Produto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Produto_ProdutoId1",
                table: "ItensPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItensPedido_ProdutoId1",
                table: "ItensPedido");

            migrationBuilder.DropColumn(
                name: "ProdutoId1",
                table: "ItensPedido");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Pedido",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Produto",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Produto",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId1",
                table: "Pedido",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Pedido",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NomeProduto",
                table: "ItensPedido",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId1",
                table: "ItensPedido",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ClienteId1",
                table: "Pedido",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId1",
                table: "ItensPedido",
                column: "PedidoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Pedido_PedidoId1",
                table: "ItensPedido",
                column: "PedidoId1",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Clientes_ClienteId1",
                table: "Pedido",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
