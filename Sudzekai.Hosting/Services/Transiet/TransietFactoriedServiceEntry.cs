using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Types.Exceptions;

namespace Sudzekai.Hosting.Services.Transiet
{
    internal class TransietFactoriedServiceEntry<TService>(object key, Func<IHostServiceProvider, TService> factoryMethod) : TransietService(key)
    {
        public override object GetInstance()
        {
            // _provider will not be null if host method RunAsync() works properly
            if (_provider is null)
                throw new ServiceInstanceCreationException(
                    $"Can't create instance of {Key} service. Host service provider was null"
                );

            var instance = factoryMethod(_provider);

            return instance is null
                ? throw new ServiceInstanceCreationException(
                    $"Can't create instance of {Key} service. Created instance was null"
                )
                : instance;
        }
    }
}
