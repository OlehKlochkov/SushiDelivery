using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.Domain.Models;


namespace SushiDelivery.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Models.Product, Id<IProductId>>, IProductRepository
    {
        public ProductRepository(ISushiDeliveryContext context) : base(context)
        {
        }

        public override async Task<Models.Product?> GetByIdAsync(Id<IProductId> id)
        {
            return await Context.GetDbSet<Models.Product>()
                .AsNoTracking()
                .Include(p => p.Ingredients)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Models.Product>> SearchByNameAsync(string name)
        {
            return await Context.Products
                 .AsNoTracking()
                 .Where(p => p.Name == name)
                 .ToListAsync();
        }
    }
}
