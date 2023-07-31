using SushiDelivery.DAL.Interfaces;

namespace SushiDelivery.DAL.Models
{
    /// <summary>
    /// DTO for IOperationResult interface.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    internal readonly struct OperationResult<TEntity> : IOperationResult<TEntity>
        where TEntity : class
    {
        public OperationResult()
            : this(null, false, 0)
        {

        }
        public OperationResult(TEntity entity)
            : this(entity, false, 1)
        {

        }

        public OperationResult(TEntity entity, bool wasOverriden)
            : this(entity, wasOverriden, 1)
        {
        }
        public OperationResult(long count)
            : this(null, false, count)
        {
        }
        public OperationResult(TEntity? entity, bool wasOverriden = false, long count = 1)
        {
            Entity = entity;
            WasOverriden = wasOverriden;
            Count = count;
        }

        public long Count { get; } = 1;

        public bool WasOverriden { get; } = false;

        public TEntity? Entity { get; } = null;

        public override string ToString() => $"{base.ToString()} {nameof(WasOverriden)}={WasOverriden} {nameof(Count)} = {Count} {nameof(Entity)} = {Entity}";
    }
}
