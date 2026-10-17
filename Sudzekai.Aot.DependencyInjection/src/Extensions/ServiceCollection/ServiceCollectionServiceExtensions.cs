using Sudzekai.Aot.DependencyInjection.Abstractions;

namespace Sudzekai.Aot.DependencyInjection
{
    public static partial class ServiceCollectionServiceExtensions
    {
        // scoped

        public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));
            
            var descriptor = ServiceDescriptor.Scoped(serviceType, implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService, IService<TImplementation>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            var descriptor = ServiceDescriptor.Scoped(typeof(TService), TImplementation.New);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddScoped<T>(this IServiceCollection services)
            where T : class, IService<T>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            var descriptor = ServiceDescriptor.Scoped<T>();

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddScoped<T>(this IServiceCollection services, Func<IServiceProvider, T> implementationFactory)
            where T: class
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.Scoped(typeof(T), implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.Scoped(typeof(TService), implementationFactory);

            services.Add(descriptor);

            return services;
        }

        // singleton

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, object instance)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(instance, nameof(instance));

            var desriptor = ServiceDescriptor.Singleton(serviceType, instance);

            services.Add(desriptor);

            return services;
        }

        public static IServiceCollection AddSingleton<T>(this IServiceCollection services, T instance)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(instance, nameof(instance));

            var desriptor = ServiceDescriptor.Singleton<T>(instance);

            services.Add(desriptor);

            return services;
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, TService instance)
            where TService : class
            where TImplementation : class, TService
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(instance, nameof(instance));

            var desriptor = ServiceDescriptor.Singleton<TService>(instance);

            services.Add(desriptor);

            return services;
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.Singleton(serviceType, implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService, IService<TImplementation>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            var descriptor = ServiceDescriptor.Singleton(typeof(TService), TImplementation.New);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddSingleton<T>(this IServiceCollection services)
            where T : class, IService<T>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            var descriptor = ServiceDescriptor.Singleton<T>();

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddSingleton<T>(this IServiceCollection services, Func<IServiceProvider, T> implementationFactory)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.Singleton(typeof(T), implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.Singleton(typeof(TService), implementationFactory);

            services.Add(descriptor);

            return services;
        }

        // transiet

        public static IServiceCollection AddTransiet(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));

            var descriptor = ServiceDescriptor.Transiet(serviceType, implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddTransiet<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService, IService<TImplementation>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            var descriptor = ServiceDescriptor.Transiet(typeof(TService), TImplementation.New);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddTransiet<T>(this IServiceCollection services)
            where T : class, IService<T>
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            var descriptor = ServiceDescriptor.Transiet<T>();

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddTransiet<T>(this IServiceCollection services, Func<IServiceProvider, T> implementationFactory)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.Transiet(typeof(T), implementationFactory);

            services.Add(descriptor);

            return services;
        }

        public static IServiceCollection AddTransiet<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            var descriptor = ServiceDescriptor.Transiet(typeof(TService), implementationFactory);

            services.Add(descriptor);

            return services;
        }
    }
}
