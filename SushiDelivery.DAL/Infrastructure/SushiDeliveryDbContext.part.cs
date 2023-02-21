using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SushiDelivery.DAL.Models;
using SushiDelivery.Domain;

namespace SushiDelivery.DAL.Infrastructure
{
    internal partial class SushiDeliveryDbContext
    {
        //protected void OnModelCreatingPartial(ModelBuilder modelBuilder)
        //{
        //    var converter = new ValueConverter<Id<ICustomerId>, Guid>(
        //      v => (Guid)v,
        //      v => (Id<ICustomerId>)v);

        //    modelBuilder.Entity<Models.Customer>(entity =>
        //    {
        //        entity.HasKey(e => e.Id).HasName("PK__Customer");
        //        entity.Property(e => e.Id).HasConversion(converter)
        //        .HasColumnType("uniqueidentifier")
        //        .IsRequired()
        //        .ValueGeneratedNever();
        //    });
        //}
    }
}