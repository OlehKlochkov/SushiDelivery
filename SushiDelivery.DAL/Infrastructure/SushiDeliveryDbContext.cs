using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Configurations;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Infrastructure
{
    /// <summary>
    /// The database context.
    /// </summary>
    internal partial class SushiDeliveryDbContext : DbContext, ISushiDeliveryContext
    {
        #region .ctor

        public SushiDeliveryDbContext(DbContextOptions<SushiDeliveryDbContext> options)
            : base(options)
        {
        }

        #endregion

        #region Properties

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Ingredient> Ingredients { get; set; }

        public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }


        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientConfiguration());
            modelBuilder.ApplyConfiguration(new ProductIngredientConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #endregion
    }
}