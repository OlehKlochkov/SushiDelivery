namespace SushiDelivery.DAL.Interfaces
{
    /// <summary>
    /// Result of the database operation.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IOperationResult<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Number of records affected (added / modifed / deleted).
        /// </summary>
        public long Count { get; }

        /// <summary>
        /// True if optimistic concurrency check failed and newer record was overwritten in database.
        /// In case of Create - true if entity with given Id already exitst and was overwritten.
        /// </summary>
        public bool WasOverriden { get; }

        /// <summary>
        /// The result entity. Contains latest data from the database.
        /// In case of Create - the entity will have the correct Id generateed in DB.
        /// In case of Update - the entity is the record in database before the update.
        /// In case of Delete - the entity is the deleted record.
        /// </summary>
        public TEntity? Entity { get; }
    }
}
