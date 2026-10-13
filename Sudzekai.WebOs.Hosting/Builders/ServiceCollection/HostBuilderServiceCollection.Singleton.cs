using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Services.Singleton;

namespace Sudzekai.Hosting.Builders.ServiceCollection
{
    public partial class HostBuilderServiceCollection
    {
        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddSingleton<TService>(TService instance)
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new SingletonInstancedServiceEntry<TService>(key, instance));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddSingleton<TBase, TService>(TService instance)
        {
            var key = typeof(TBase);

            ThrowIfRegistered(key);

            _services.Add(new SingletonInstancedServiceEntry<TService>(key, instance));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddSingleton<TService>()
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new SingletonGenericServiceEntry<TService>(key));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddSingleton<TBase, TService>()
        {
            var key = typeof(TBase);

            ThrowIfRegistered(key);

            _services.Add(new SingletonGenericServiceEntry<TService>(key));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddSingletonFactory<TService>(IServiceFactory<TService> factory)
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new SingletonFactoriedServiceEntry<TService>(key, factory.Create));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddSingletonFactory<TService>(Func<IHostServiceProvider, TService> factoryMethod)
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new SingletonFactoriedServiceEntry<TService>(key, factoryMethod));

            return this;
        }
    }
}
