namespace SushiDelivery.DAL.Infrastructure
{
    /// <summary>
    /// Factory to create a new <see cref="ISushiDeliveryContext"/>.
    /// </summary>
    internal interface IContextFactory
    {
        ISushiDeliveryContext CreateDbContext();
    }
}
