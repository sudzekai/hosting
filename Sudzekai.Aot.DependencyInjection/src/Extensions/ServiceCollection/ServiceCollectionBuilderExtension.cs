using Sudzekai.Aot.DependencyInjection.Abstractions;

namespace Sudzekai.Aot.DependencyInjection.src.Extensions
{
    public static class ServiceCollectionBuilderExtension
    {
        public static ServiceProvider BuildServiceProvider(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            return new(services);
        }
    }
}
