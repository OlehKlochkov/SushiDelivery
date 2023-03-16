namespace SushiDelivery.DAL.Interfaces
{
    public interface ICreateCommand<TEntity>
        where TEntity : class
    {
        TEntity Entity { get; }
    }
}
