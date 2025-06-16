using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoCompra.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PedidoCompra.Infrastructure.Data
{
    public class PedidoCompraContext : DbContext
    {
        public PedidoCompraContext(DbContextOptions<PedidoCompraContext> options)
            : base(options)
        {
        }
        // DbSets
         public DbSet<Pedido> Pedidos { get; set; }
         public DbSet<ItemPedido> ItensPedido { get; set; }
         public DbSet<Pedido> Cliente { get; set; }
         public DbSet<Pedido> Produto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoCompraContext).Assembly);

            base.OnModelCreating(modelBuilder);

        }
    }
}
