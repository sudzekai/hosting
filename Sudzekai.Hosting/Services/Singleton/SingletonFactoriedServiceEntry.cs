using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Types.Exceptions;

namespace Sudzekai.Hosting.Services.Singleton
{
    internal class SingletonFactoriedServiceEntry<TService>(object key, Func<IHostServiceProvider, TService> factoryMethod) : SingletonServiceEntry(key)
        where TService : class
    {
        private TService? _instance;

        public override object GetInstance()
        {
            // _provider will not be null if host method RunAsync() works properly
            if (_provider is null)
                throw new ServiceInstanceCreationException(
                    $"Can't create instance of {Key} service. Host service provider was null"
                );

            _instance ??= factoryMethod(_provider);

            return _instance is null
                ? throw new ServiceInstanceCreationException(
                    $"Can't create instance of {Key} service. Created instance was null"
                )
                : _instance;
        }
    }
}
