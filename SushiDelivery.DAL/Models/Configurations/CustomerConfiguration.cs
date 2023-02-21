﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SushiDelivery.Domain;

#nullable disable

namespace SushiDelivery.DAL.Models.Configurations
{
    public partial class CustomerConfiguration : ConfigurationBase, IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            var converter = new ValueConverter<Id<ICustomerId>, Guid>(
                v => (Guid)v,
                v => (Id<ICustomerId>)v);

            entity.HasKey(e => e.Id).IsClustered(true).HasName("PK_Customer_Id");
            entity.HasAlternateKey(e => e.LoginName).IsClustered(false);
            entity.Property(e => e.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                .ValueGeneratedNever()
                .HasConversion(converter)
                .HasColumnOrder(1);

            entity.Property(e => e.LoginName).IsRequired(true).HasColumnOrder(2);
            entity.Property(e => e.Phone).HasColumnOrder(3);
            entity.Property(e => e.Address).HasColumnOrder(4);

            base.ConfigureBase(entity);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Customer> entity);
    }
}
