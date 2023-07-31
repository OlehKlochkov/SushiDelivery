using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Infrastructure
{
    /// <summary>
    /// EF Field converted implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal static class ValueConverterHelper<T>
    {
        /// <summary>
        /// Convert between Guid and EntityId.
        /// </summary>
        /// <returns></returns>
        public static ValueConverter<Id<T>, Guid> GetValueConverter() => new(v => (Guid)v, v => (Id<T>)v);
    }
}
