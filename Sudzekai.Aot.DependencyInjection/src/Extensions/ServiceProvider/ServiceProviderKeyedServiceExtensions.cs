namespace Sudzekai.Aot.DependencyInjection
{
    public static class ServiceProviderKeyedServiceExtensions
    {
        public static T? GetKeyedService<T>(this IServiceProvider provider, object? serviceKey)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            if (provider is IKeyedServiceProvider keyedSp)
                return (T?)keyedSp.GetKeyedService(typeof(T), serviceKey);

            throw new InvalidOperationException("Provider is not keyed service provider");
        }

        public static object? GetKeyedService(this IServiceProvider provider, Type serviceType, object? serviceKey)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));

            if (provider is IKeyedServiceProvider keyedSp)
                return keyedSp.GetKeyedService(serviceType, serviceKey);

            throw new InvalidOperationException("Provider is not keyed service provider");
        }

        public static T GetRequiredKeyedService<T>(this IServiceProvider provider, object? serviceKey)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            if (provider is IKeyedServiceProvider keyedSp)
                return (T)keyedSp.GetRequiredKeyedService(typeof(T), serviceKey);

            throw new InvalidOperationException("Provider is not keyed service provider");
        }

        public static object GetRequiredKeyedService(this IServiceProvider provider, Type serviceType, object? serviceKey)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));

            if (provider is IKeyedServiceProvider keyedSp)
                return keyedSp.GetRequiredKeyedService(serviceType, serviceKey);

            throw new InvalidOperationException("Provider is not keyed service provider");
        }
    }
}
