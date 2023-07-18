using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Interfaces
{
    public interface IProductRepository : IRepository<IProduct, IProductId>, IQueryEngine<IProduct>
    {
        public Task<IEnumerable<IProduct>> SearchByNameAsync(string name);
    }
}
