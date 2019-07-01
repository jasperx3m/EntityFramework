﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickReach.ECommerce.Infra.Data;

namespace QuickReach.ECommerce.Infra.Data.Migrations
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20190701075448_Added ProductSupplier")]
    partial class AddedProductSupplier
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.CategoryRollup", b =>
                {
                    b.Property<int>("ParentCategoryID");

                    b.Property<int>("ChildCategoryID");

                    b.HasKey("ParentCategoryID", "ChildCategoryID");

                    b.HasIndex("ChildCategoryID");

                    b.ToTable("CategoryRollup");
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<decimal>("Price");

                    b.HasKey("ID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.ProductCategory", b =>
                {
                    b.Property<int>("CategoryID");

                    b.Property<int>("ProductID");

                    b.HasKey("CategoryID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.ProductSupplier", b =>
                {
                    b.Property<int>("SupplierID");

                    b.Property<int>("ProductID");

                    b.HasKey("SupplierID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductSupplier");
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("ID");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.CategoryRollup", b =>
                {
                    b.HasOne("QuickReach.ECommerce.Domain.Models.Category", "ChildCategory")
                        .WithMany("ParentCategories")
                        .HasForeignKey("ChildCategoryID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("QuickReach.ECommerce.Domain.Models.Category", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategoryID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.ProductCategory", b =>
                {
                    b.HasOne("QuickReach.ECommerce.Domain.Models.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("QuickReach.ECommerce.Domain.Models.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.ProductSupplier", b =>
                {
                    b.HasOne("QuickReach.ECommerce.Domain.Models.Product", "Product")
                        .WithMany("ProductSuppliers")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("QuickReach.ECommerce.Domain.Models.Supplier", "Supplier")
                        .WithMany("ProductSuppliers")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
