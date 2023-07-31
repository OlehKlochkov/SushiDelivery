namespace SushiDelivery.DAL.Interfaces
{
    /// <summary>
    /// Povides acces to Queryable set or entitites.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IQueryEngine<TEntity> : IDisposable
        where TEntity : class
    {
        /// <summary>
        /// Items to query by LINQ.
        /// Note: postponed execution!
        /// </summary>
        public IQueryable<TEntity> Items { get; }
    }
}
