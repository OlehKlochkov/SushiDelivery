using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SushiDelivery.DAL.Configurations;
using SushiDelivery.DAL.Models;
using SushiDelivery.DAL.Infrastructure;

namespace SushiDelivery.DAL.Migrations
{
    internal class MigrationsDbContext : SushiDeliveryDbContext
    {
        public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options)
            : base(options)
        {
        }
    }
}
