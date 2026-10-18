using Sudzekai.Aot.DependencyInjection.Abstractions;

namespace Sudzekai.Aot.DependencyInjection
{
    public static partial class ServiceCollectionServiceExtensions
    {
        // scoped

        public static IServiceCollection AddKeyedScoped(this IServiceCollection services, Type serviceType, object serviceKey, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.KeyedScoped(serviceType, serviceKey, implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedScoped<TService, TImplementation>(this IServiceCollection services, object serviceKey)
            where TService : class
            where TImplementation : class, TService, IService<TImplementation>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));

            var descriptor = ServiceDescriptor.KeyedScoped(typeof(TService), serviceKey, TImplementation.New);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedScoped<T>(this IServiceCollection services, object serviceKey)
            where T : class, IService<T>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));

            var descriptor = ServiceDescriptor.KeyedScoped<T>(serviceKey);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedScoped<T>(this IServiceCollection services, object serviceKey, Func<IServiceProvider, T> implementationFactory)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.KeyedScoped<T>(serviceKey, implementationFactory);

            services.Add(descriptor);

            return services;
        }

        // singleton

        public static IServiceCollection AddKeyedSingleton(this IServiceCollection services, Type serviceType, object serviceKey, object instance)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(instance, nameof(instance));

            var desriptor = ServiceDescriptor.KeyedSingleton(serviceType, serviceKey, instance);

            services.Add(desriptor);

            return services;
        }

        public static IServiceCollection AddKeyedSingleton<T>(this IServiceCollection services, object serviceKey, T instance)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(instance, nameof(instance));

            var desriptor = ServiceDescriptor.KeyedSingleton<T>(serviceKey, instance);

            services.Add(desriptor);

            return services;
        }

        public static IServiceCollection AddKeyedSingleton(this IServiceCollection services, Type serviceType, object serviceKey, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.KeyedSingleton(serviceType, serviceKey, implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedSingleton<TService, TImplementation>(this IServiceCollection services, object serviceKey)
            where TService : class
            where TImplementation : class, TService, IService<TImplementation>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));

            var descriptor = ServiceDescriptor.KeyedSingleton(typeof(TService), serviceKey, TImplementation.New);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedSingleton<T>(this IServiceCollection services, object serviceKey)
            where T : class, IService<T>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));

            var descriptor = ServiceDescriptor.KeyedSingleton<T>(serviceKey);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedSingleton<T>(this IServiceCollection services, object serviceKey, Func<IServiceProvider, T> implementationFactory)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.KeyedSingleton(typeof(T), serviceKey, implementationFactory);

            services.Add(descriptor);

            return services;
        }

        // transiet

        public static IServiceCollection AddKeyedTransiet(this IServiceCollection services, Type serviceType, object serviceKey, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.KeyedTransiet(serviceType, serviceKey, implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedTransiet<TService, TImplementation>(this IServiceCollection services, object serviceKey)
            where TService : class
            where TImplementation : class, TService, IService<TImplementation>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));

            var descriptor = ServiceDescriptor.KeyedTransiet(typeof(TService), serviceKey, TImplementation.New);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedTransiet<T>(this IServiceCollection services, object serviceKey)
            where T : class, IService<T>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));

            var descriptor = ServiceDescriptor.KeyedTransiet<T>(serviceKey);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddKeyedTransiet<T>(this IServiceCollection services, object serviceKey, Func<IServiceProvider, T> implementationFactory)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.KeyedTransiet(typeof(T), serviceKey, implementationFactory);

            services.Add(descriptor);

            return services;
        }
    }
}
