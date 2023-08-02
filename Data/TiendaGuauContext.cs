using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TiendaGuau.Models;

    public class TiendaGuauContext : DbContext
    {
        public TiendaGuauContext (DbContextOptions<TiendaGuauContext> options)
            : base(options)
        {
        }

        public DbSet<TiendaGuau.Models.Client> Client { get; set; } = default!;

        public DbSet<TiendaGuau.Models.Product> Product { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Client>(client =>
        {
            client.ToTable("Client");
            client.HasKey(p => p.ClientId);
            client.Property(p => p.ClientId).ValueGeneratedOnAdd();
            client.Property(p => p.NameClient).IsRequired().HasMaxLength(150);
            client.Property(p => p.LastnameClient).IsRequired().HasMaxLength(150);
            client.HasIndex(p => p.DNIClient).IsUnique();
            client.Property(p => p.AdressClient).IsRequired();
            client.Property(p => p.Phone).IsRequired();

        });

        modelBuilder.Entity<Product>(product =>
        {
            product.ToTable("Product");
            product.HasKey(p => p.ProductId);
            product.Property(p => p.ProductId).ValueGeneratedOnAdd();
            product.HasOne(p => p.Client).WithMany(p => p.Product).HasForeignKey(p => p.ClientId);
            product.Property(p => p.NameProduct).IsRequired().HasMaxLength(150);
            product.Property(p => p.Description).IsRequired(false);
            product.Property(p => p.Price).IsRequired();
        });

    }
}
