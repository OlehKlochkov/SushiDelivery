using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Infrastructure
{
    internal static class ValueConverterHelper<T>
    {
        public static ValueConverter<Id<T>, Guid> GetValueConverter() => new(v => (Guid)v, v => (Id<T>)v);
    }
}
