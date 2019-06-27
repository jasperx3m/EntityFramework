using Microsoft.EntityFrameworkCore;
using System;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data.EntityConfiguration;
using System.Linq;

namespace QuickReach.ECommerce.Infra.Data
{
    public class ECommerceDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryRollupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryEntityTypeConfiguration());
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
          
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }
        public ECommerceDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=.;Database=QuickReachDb;Integrated Security=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
