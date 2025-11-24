using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tienda.Models;

namespace tienda.Data
{
    public class TiendaContext: DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options){}
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Serie> Series { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Serie>()
                .HasOne(s => s.Producto)
                .WithMany(p => p.Series)
                .HasForeignKey(s => s.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Serie>()
                .HasIndex(s => s.NroSerie)
                .IsUnique();
        }


    }
}
