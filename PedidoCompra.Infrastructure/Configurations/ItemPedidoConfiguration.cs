using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PedidoCompra.Infrastructure.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantidade)
                .IsRequired();

            builder.Property(i => i.ValorTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(i => i.PedidoId)
                .IsRequired();

            builder.Property(i => i.ProdutoId)
                .IsRequired();

            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Navigation(i => i.Pedido)
                .AutoInclude(false);

            builder.Navigation(i => i.Produto)
                .AutoInclude(false);

            // Adiciona um índice composto para PedidoId e ProdutoId
            builder.HasIndex(i => new { i.PedidoId, i.ProdutoId })
                .IsUnique();
        }
    }
}
