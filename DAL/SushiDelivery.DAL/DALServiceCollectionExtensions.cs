using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Interfaces;
using SushiDelivery.DAL.Repositories;

namespace SushiDelivery.DAL
{
    /// <summary>
    /// Extension menthids for DAL interfaces registration in DI container.
    /// </summary>
    public static class DALServiceCollectionExtensions
    {
        public static void AddDALSql(this IServiceCollection services, IConfiguration configuration)
        {
#pragma warning disable IDE0058 // Expression value is never used
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISushiDeliveryContext, SushiDeliveryDbContext>();
            services.AddScoped<ISushiDeliveryContext, SushiDeliveryDbContext>();
            services.AddDbContext<SushiDeliveryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
#pragma warning disable CS8621 // Nullability of reference types in return type doesn't match the target delegate (possibly because of nullability attributes).
            services.AddTransient(provider => new Lazy<ISushiDeliveryContext>(provider.GetService<ISushiDeliveryContext>));
            services.AddTransient(provider => new Lazy<IUnitOfWork>(provider.GetService<IUnitOfWork>));
            services.AddTransient(provider => new Lazy<IProductRepository>(provider.GetService<IProductRepository>));
            services.AddTransient(provider => new Lazy<ICustomerRepository>(provider.GetService<ICustomerRepository>));
#pragma warning restore CS8621 // Nullability of reference types in return type doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning restore IDE0058 // Expression value is never used
        }
    }
}
