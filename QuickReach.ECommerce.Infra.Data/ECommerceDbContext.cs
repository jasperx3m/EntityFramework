using Microsoft.EntityFrameworkCore;
using System;
using QuickReach.ECommerce.Domain.Models;


namespace QuickReach.ECommerce.Infra.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
                    
        }
        public ECommerceDbContext() : base()
        {

        }
        
        public DbSet<Category> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=.;Database=QuickReachDb;Integrated Security=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
