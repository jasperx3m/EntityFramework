using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.EntityConfiguration
{
    public 
        class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.Property(c => c.ID)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
            builder.HasMany(c => c.Orders);

        }
    }
}
