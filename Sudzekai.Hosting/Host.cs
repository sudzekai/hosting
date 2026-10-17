using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Hosts;

namespace Sudzekai.Hosting
{
    internal class Host(HostServiceProvider services) : IHost
    {
        private HostServiceProvider _provider = services;

        public IConfiguration Configuration { get => Services.GetRequiredService<IConfiguration>(); }

        public IHostServiceProvider Services { get => _provider; }

        private bool _disposed = false;

        public async ValueTask DisposeAsync()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _disposed = true;

            await _provider.DisposeAllServicesAsync();
        }

        public async Task RunAsync()
        {
            _provider.SetServiceProvider(Services);
            await _provider.StartHostedServicesAsync(_provider);
        }

        public async Task StopAsync()
        {
            await _provider.StopHostedServicesAsync();
        }
    }
}
