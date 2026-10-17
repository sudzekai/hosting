namespace Sudzekai.Aot.DependencyInjection
{
    public static class ServiceProviderServiceExtension
    {
        public static T? GetService<T>(this IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            return (T?)provider.GetService(typeof(T));
        }

        public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));

            return provider.GetService(serviceType) ??
                throw new InvalidOperationException($"No service with type `{serviceType}` registered");
        }

        public static T GetRequiredService<T>(this IServiceProvider provider)
            => (T)provider.GetRequiredService(typeof(T));
    }
}
