using Sudzekai.Hosting.Services.Abstractions;

namespace Sudzekai.Hosting.Services.Singleton
{
    internal abstract class SingletonServiceEntry(object key) : ServiceEntry(key);
}
