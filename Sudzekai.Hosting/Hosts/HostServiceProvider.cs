using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Services.Abstractions;
using Sudzekai.Hosting.Services.Hosted;
using Sudzekai.Hosting.Services.Singleton;
using Sudzekai.Hosting.Types.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Sudzekai.Hosting.Hosts
{
    internal class HostServiceProvider(IReadOnlyCollection<IServiceEntry> services) : IHostServiceProvider
    {
        private IReadOnlyCollection<IServiceEntry> _services = services;

        public void SetServiceProvider(IHostServiceProvider provider)
        {
            foreach (var service in _services)
            {
                service.SetServiceProvider(provider);
            }
        }

        public async Task StartHostedServicesAsync(IHostServiceProvider provider)
        {
            var hostedServices = _services.OfType<HostedServiceEntry>().ToList();

            foreach (var service in hostedServices)
            {
                var hosted = service.GetInstance<IHostedService>();

                await hosted.RunAsync();
            }
        }

        public async Task StopHostedServicesAsync()
        {
            var hostedServices = _services.OfType<HostedServiceEntry>().ToList();

            foreach (var service in hostedServices)
            {
                var hosted = service.GetInstance<IHostedService>();

                await hosted.StopAsync();
            }
        }

        public async Task DisposeAllServicesAsync()
        {
            List<IServiceEntry> services = [];

            services.AddRange(_services.OfType<SingletonServiceEntry>());
            services.AddRange(_services.OfType<HostedServiceEntry>());

            foreach (var service in services)
            {
                var instance = service.GetInstance<object>();

                if (instance is IAsyncDisposable asyncDisposableService)
                    await asyncDisposableService.DisposeAsync();

                else if (instance is IDisposable disposableService)
                    disposableService.Dispose();
            }
        }

        public T GetRequiredService<T>()
        {
            var existing = _services.FirstOrDefault(s => s.Key.Equals(typeof(T)))
                ?? throw new ServiceNotFoundException(typeof(T).ToString());

            return existing.GetInstance<T>();
        }

        public bool TryGetRequiredService<T>([NotNullWhen(true)] out T? service)
        {
            service = default;

            var existing = _services.FirstOrDefault(s => s.Key.Equals(typeof(T)));

            if (existing == null)
                return false;

            try
            {
                service = existing.GetInstance<T>();
            }
            catch
            {
                return false;
            }

            if (service is not null)
                return true;

            return false;
        }
    }
}
