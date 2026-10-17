using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Services.Hosted;

namespace Sudzekai.Hosting.Builders.ServiceCollection
{
    public partial class HostBuilderServiceCollection
    {
        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddHosted<TService>(TService instance)
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new HostedInstancedServiceEntry<TService>(key, instance));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddHosted<TBase, TService>(TService instance)
        {
            var key = typeof(TBase);

            ThrowIfRegistered(key);

            _services.Add(new HostedInstancedServiceEntry<TService>(key, instance));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddHosted<TService>()
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new HostedGenericServiceEntry<TService>(key));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddHosted<TBase, TService>()
        {
            var key = typeof(TBase);

            ThrowIfRegistered(key);

            _services.Add(new HostedGenericServiceEntry<TService>(key));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddHostedFactory<TService>(IServiceFactory<TService> factory)
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new HostedFactoriedServiceEntry<TService>(key, factory.Create));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddHostedFactory<TService>(Func<IHostServiceProvider, TService> factoryMethod)
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new HostedFactoriedServiceEntry<TService>(key, factoryMethod));

            return this;
        }
    }
}
