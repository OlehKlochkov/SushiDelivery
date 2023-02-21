using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Models.Configurations;

namespace SushiDelivery.DAL.Infrastructure
{
    internal partial class SushiDeliveryDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}