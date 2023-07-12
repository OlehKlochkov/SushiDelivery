namespace SushiDelivery.DAL.Repositories
{
    /// <summary>
    /// Interface for base repository for database operations.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TEntityId">Type of entity Id.</typeparam>
    interface IRepository<TEntity, TEntityId>
        where TEntity : class
        where TEntityId : struct
    {
        Task<TEntity?> GetByIdAsync(TEntityId id);

        Task SaveAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
    }
}
