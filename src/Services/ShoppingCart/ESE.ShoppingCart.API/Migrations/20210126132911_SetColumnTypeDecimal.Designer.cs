﻿// <auto-generated />
using System;
using ESE.ShoppingCart.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ESE.ShoppingCart.API.Migrations
{
    [DbContext(typeof(ShoppingCartContext))]
    [Migration("20210126132911_SetColumnTypeDecimal")]
    partial class SetColumnTypeDecimal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ESE.ShoppingCart.API.Models.CustomerCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .HasName("IDX_Customer");

                    b.ToTable("CustomerCarts");
                });

            modelBuilder.Entity("ESE.ShoppingCart.API.Models.ItemCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerCartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerCartId");

                    b.ToTable("ItemCarts");
                });

            modelBuilder.Entity("ESE.ShoppingCart.API.Models.ItemCart", b =>
                {
                    b.HasOne("ESE.ShoppingCart.API.Models.CustomerCart", "CustomerCart")
                        .WithMany("Items")
                        .HasForeignKey("CustomerCartId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
