using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Interfaces
{
    /// <summary>
    /// Interface for base repository for database CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TEntityId">Type of entity Id.</typeparam>
    public interface IRepository<TEntity, TEntityId> : IDisposable
        where TEntityId : class
        where TEntity : class, TEntityId
    {
        /// <summary>
        /// Save each operation to database or use Unit Of Work.
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        /// <summary>
        /// Search entity by primary key value.
        /// </summary>
        /// <param name="id">Primary key value.</param>
        /// <returns>Entity or NULL if not found.</returns>
        Task<TEntity?> GetByIdAsync(Id<TEntityId> id);

        /// <summary>
        /// Creates new entity and returns instance with new ID generated.
        /// OR if ID already exists - returns the existing one.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        /// <returns>Operation result.</returns>
        Task<IOperationResult<TEntity>> CreateAsync(TEntity entity);

        /// <summary>
        /// Updates the entity found by ID.
        /// </summary>
        /// <param name="entity">Entity with new values.</param>
        /// <returns>Operation result.</returns>
        Task<IOperationResult<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes entity by id.
        /// </summary>
        /// <param name="id">Entity primary key.</param>
        /// <returns>Operation result.</returns>
        Task<IOperationResult<TEntity>> DeleteAsync(Id<TEntityId> id);
    }
}
