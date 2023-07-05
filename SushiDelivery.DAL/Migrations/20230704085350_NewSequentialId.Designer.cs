﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SushiDelivery.DAL.Infrastructure;

#nullable disable

namespace SushiDelivery.DAL.Migrations
{
    [DbContext(typeof(SushiDeliveryDbContext))]
    [Migration("20230704085350_NewSequentialId")]
    partial class NewSequentialId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SushiDelivery.DAL.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1)
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Address")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnOrder(4);

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(101)
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTimeOffset>("DeletedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(103)
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnOrder(104)
                        .HasDefaultValueSql("0");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnOrder(2);

                    b.Property<string>("Phone")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnOrder(3);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnOrder(105);

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(102)
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id")
                        .HasName("PK_Customer_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.HasAlternateKey("LoginName");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasAlternateKey("LoginName"), false);

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SushiDelivery.DAL.Models.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1)
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(101)
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTimeOffset>("DeletedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(103)
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnOrder(104)
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnOrder(105);

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(102)
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id")
                        .HasName("PK__Ingredient_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("SushiDelivery.DAL.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1)
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<int>("Category")
                        .HasColumnType("int")
                        .HasColumnOrder(5);

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(101)
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTimeOffset>("DeletedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(103)
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnOrder(104)
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnOrder(3);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnOrder(105);

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(102)
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id")
                        .HasName("PK__Product_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SushiDelivery.DAL.Models.ProductIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(3);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.HasKey("Id")
                        .HasName("PK__ProductIngredient_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.HasAlternateKey("ProductId", "IngredientId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasAlternateKey("ProductId", "IngredientId"), false);

                    b.HasIndex("IngredientId");

                    b.ToTable("ProductIngredients");
                });

            modelBuilder.Entity("SushiDelivery.DAL.Models.ProductIngredient", b =>
                {
                    b.HasOne("SushiDelivery.DAL.Models.Ingredient", "Ingredient")
                        .WithMany("ProductIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SushiDelivery.DAL.Models.Product", "Product")
                        .WithMany("ProductIngredients")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SushiDelivery.DAL.Models.Ingredient", b =>
                {
                    b.Navigation("ProductIngredients");
                });

            modelBuilder.Entity("SushiDelivery.DAL.Models.Product", b =>
                {
                    b.Navigation("ProductIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
