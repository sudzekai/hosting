namespace Sudzekai.Aot.DependencyInjection.Abstractions
{
    public interface IService<TService>
        where TService : class, IService<TService>
    {
        static abstract TService New(IServiceProvider sp);
    }
}
