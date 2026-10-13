using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Types.Exceptions;

namespace Sudzekai.Hosting.Services.Abstractions
{
    internal abstract class ServiceEntry(object key) : IServiceEntry
    {
        protected IHostServiceProvider? _provider;

        public virtual object Key { get; init; } = key;

        public abstract object GetInstance();

        public virtual T GetInstance<T>()
        {
            try
            {
                return (T)GetInstance();
            }
            catch (Exception ex)
            {
                throw new ServiceInstanceCreationException($"Can't create instance of {Key} service. {ex.Message}");
            }
        }

        public virtual void SetServiceProvider(IHostServiceProvider provider)
            => _provider = provider;
    }
}
