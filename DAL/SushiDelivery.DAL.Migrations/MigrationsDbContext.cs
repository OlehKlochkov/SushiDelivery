using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SushiDelivery.DAL.Configurations;
using SushiDelivery.DAL.Models;
using SushiDelivery.DAL.Infrastructure;

namespace SushiDelivery.DAL.Migrations
{
    /// <summary>
    /// Child DB context only for applying DB migrations.
    /// </summary>
    internal class MigrationsDbContext : SushiDeliveryDbContext
    {
        public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options)
            : base(options)
        {
        }
    }
}
