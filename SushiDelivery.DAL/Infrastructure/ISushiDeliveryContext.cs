using Microsoft.EntityFrameworkCore;

namespace SushiDelivery.DAL.Infrastructure
{
    /// <summary>
    /// Interface for the database context.
    /// </summary>
    internal interface ISushiDeliveryContext : IDisposable
    {
        #region Properties

        DbSet<Models.Customer> Customers { get; set; }

        #endregion
    }
}
