namespace SushiDelivery.DAL.Interfaces
{
    public interface ICreateCommandResult<TEntity>
            where TEntity : class
    {
        TEntity Entity { get; }
    }
}
