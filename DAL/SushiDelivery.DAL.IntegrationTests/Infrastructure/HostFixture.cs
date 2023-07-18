using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SushiDelivery.DAL.IntegrationTests.Infrastructure
{
    public class HostFixture : IDisposable, IAsyncLifetime
    {
        public IHost TheHost { get; private set; }

        public async Task InitializeAsync() => TheHost = CreateHostBuilder(Array.Empty<string>()).Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices( (hostContext, services) =>
                    {
                        services.AddTransient((services) => services.GetService<ILoggerFactory>().CreateLogger("TEST"));
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
