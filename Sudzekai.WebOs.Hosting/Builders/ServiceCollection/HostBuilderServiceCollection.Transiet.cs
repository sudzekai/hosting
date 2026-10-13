using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Services.Transiet;

namespace Sudzekai.Hosting.Builders.ServiceCollection
{
    public partial class HostBuilderServiceCollection
    {
        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddTransiet<TService>()
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new TransietGenericServiceEntry<TService>(key));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddTransiet<TBase, TService>()
        {
            var key = typeof(TBase);

            ThrowIfRegistered(key);

            _services.Add(new TransietGenericServiceEntry<TService>(key));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddTransietFactory<TService>(IServiceFactory<TService> factory)
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new TransietFactoriedServiceEntry<TService>(key, factory.Create));

            return this;
        }

        IHostBuilderServiceCollection IHostBuilderServiceCollection.AddTransietFactory<TService>(Func<IHostServiceProvider, TService> factoryMethod)
        {
            var key = typeof(TService);

            ThrowIfRegistered(key);

            _services.Add(new TransietFactoriedServiceEntry<TService>(key, factoryMethod));

            return this;
        }
    }
}
