﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickReach.ECommerce.Infra.Data;

namespace QuickReach.ECommerce.Infra.Data.Migrations
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20190703063657_Added some changes")]
    partial class Addedsomechanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.Cart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BuyerId");

                    b.HasKey("ID");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CartID");

                    b.Property<decimal>("OldUnitPrice");

                    b.Property<string>("PictureUrl");

                    b.Property<int>("ProductId");

                    b.Property<string>("ProductName");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("CartID");

                    b.ToTable("CartItem");
                });

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

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CardHolderName")
                        .IsRequired();

                    b.Property<string>("CardNumber")
                        .IsRequired();

                    b.Property<int>("CardType");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Expiration")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("SecurityNumber")
                        .IsRequired();

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.Manufacturer", b =>
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

                    b.ToTable("Manufacturers");
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

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.ProductManufacturer", b =>
                {
                    b.Property<int>("ManufacturerID");

                    b.Property<int>("ProductID");

                    b.HasKey("ManufacturerID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductManufacturer");
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

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.CartItem", b =>
                {
                    b.HasOne("QuickReach.ECommerce.Domain.Models.Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Restrict);
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

            modelBuilder.Entity("QuickReach.ECommerce.Domain.Models.ProductManufacturer", b =>
                {
                    b.HasOne("QuickReach.ECommerce.Domain.Models.Manufacturer", "Manufacturer")
                        .WithMany("ProductManufacturers")
                        .HasForeignKey("ManufacturerID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("QuickReach.ECommerce.Domain.Models.Product", "Product")
                        .WithMany("ProductManufacturers")
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
