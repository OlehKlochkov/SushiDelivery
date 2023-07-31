using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Interfaces;
using SushiDelivery.Domain.Models;


namespace SushiDelivery.DAL.Repositories
{
    internal class ProductRepository : GenericRepository<Models.Product, IProduct, IProductId>, IProductRepository
    {
        public ProductRepository(Lazy<ISushiDeliveryContext> lazyContext, ILogger logger, bool autoSaveChanges = true)
            : base(lazyContext, logger, autoSaveChanges)
        {
        }

        public IQueryable<IProduct> Items => Context.Products;

        public override async Task<IProduct?> GetByIdAsync(Id<IProductId> id)
        {
            return await Context.GetDbSet<Models.Product>()
                .AsNoTracking()
                .Include(p => p.Ingredients)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<IProduct>> SearchByNameAsync(string name)
        {
            return await Context.Products
                 .AsNoTracking()
                 .Where(p => EF.Functions.Like(p.Name, name))
                 .ToListAsync();
        }

        protected override Models.Product Create(IProduct entityIn) => new Models.Product(entityIn);

        protected override void Map(Models.Product entityOut, IProduct entityIn)
        {
            entityOut.Name = entityIn.Name;
            entityOut.Price = entityIn.Price;
            entityOut.Category = entityIn.Category;
        }
    }
}
