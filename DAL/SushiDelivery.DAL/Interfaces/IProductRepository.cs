using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Interfaces
{
    public interface IProductRepository : IRepository<IProduct, IProductId>, IQueryEngine<IProduct>
    {
        /// <summary>
        /// Searches product by name. Wildcard search is supported (SQL LIKE).
        /// </summary>
        /// <param name="name">Name to find or wildcard pattern</param>
        /// <returns>Sequence of products.</returns>
        public Task<IEnumerable<IProduct>> SearchByNameAsync(string name);
    }
}
