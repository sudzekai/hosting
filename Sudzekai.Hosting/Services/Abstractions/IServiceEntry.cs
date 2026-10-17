using Sudzekai.Hosting.Abstractions;

namespace Sudzekai.Hosting.Services.Abstractions
{
    internal interface IServiceEntry
    {
        object Key { get; }
        object GetInstance();
        T GetInstance<T>();
        void SetServiceProvider(IHostServiceProvider provider);
    }
}
