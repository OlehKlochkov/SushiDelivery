using SushiDelivery.DAL.Interfaces;
using SushiDelivery.Domain.Models;

using Xunit.Abstractions;

namespace SushiDelivery.DAL.IntegrationTests.Infrastructure
{
    [Collection("Integration")]
    public class IntegrationTestBase : IDisposable
    {
        public IntegrationTestBase(HostFixture fixture, ITestOutputHelper output)
        {
            Services = fixture.TheHost.Services;
            Log = output;
            Log.WriteLine($"{GetType().FullName}: {"Constructor"}");
        }

        protected IServiceProvider Services { get; }

        protected ITestOutputHelper Log { get; }
        protected static string GenerateString() => Guid.NewGuid().ToString().Replace("-", string.Empty);
        protected static void AssertOperationResult(IOperationResult<ICustomer> result)
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Entity);
            Assert.NotEqual(Guid.Empty, (Guid)result.Entity.Id);
            Assert.False(result.WasOverriden);
            Assert.Equal(1, result.Count);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Log?.WriteLine($"{GetType().FullName}: {"Destructor"}");
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
