using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorPedidos.Infrastructure.Data
{
    public class GestorPedidoContext : DbContext
    {
        public GestorPedidoContext(DbContextOptions<GestorPedidoContext> options)
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestorPedidoContext).Assembly);

            base.OnModelCreating(modelBuilder);

        }
    }
}
