﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Interfaces;
using SushiDelivery.DAL.Repositories;

namespace SushiDelivery.DAL
{
    public static class DALServiceCollectionExtensions
    {
        public static void AddDALSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISushiDeliveryContext, SushiDeliveryDbContext>();
            services.AddScoped<ISushiDeliveryContext, SushiDeliveryDbContext>();
            services.AddDbContext<SushiDeliveryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient(provider => new Lazy<ISushiDeliveryContext>(provider.GetService<ISushiDeliveryContext>));
            services.AddTransient(provider => new Lazy<IUnitOfWork>(provider.GetService<IUnitOfWork>));
            services.AddTransient(provider => new Lazy<IProductRepository>(provider.GetService<IProductRepository>));
            services.AddTransient(provider => new Lazy<ICustomerRepository>(provider.GetService<ICustomerRepository>));
        }
    }
}