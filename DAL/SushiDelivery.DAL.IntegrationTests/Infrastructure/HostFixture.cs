using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SushiDelivery.DAL.IntegrationTests.Infrastructure
{
    public class HostFixture : IDisposable, IAsyncLifetime
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IHost TheHost { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task InitializeAsync() => TheHost = CreateHostBuilder(Array.Empty<string>()).Build();
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices( (hostContext, services) =>
                    {
                        _ = services.AddTransient((services) => services.GetService<ILoggerFactory>()!.CreateLogger("TEST"));
                        services.AddDALSql(hostContext.Configuration);
                    });

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                TheHost?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public Task DisposeAsync() => TheHost.StopAsync();
    }
}
