using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QuickReach.ECommerce.Infra.Data.EntityConfiguration
{
    public class ProductSupplierEntityTypeConfiguration : IEntityTypeConfiguration<ProductSupplier>
    {
        public void Configure(EntityTypeBuilder<ProductSupplier> builder)
        {
            builder.ToTable("ProductSupplier");
            builder.HasKey(sr => new { sr.SupplierID, sr.ProductID });
            builder.HasOne(sr => sr.Supplier)
                   .WithMany(c => c.ProductSuppliers)
                   .HasForeignKey("SupplierID");
            builder.HasOne(cr => cr.Product)
                   .WithMany(c => c.ProductSuppliers)
                   .HasForeignKey("ProductID");
        }
    }
}
