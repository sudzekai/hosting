namespace Sudzekai.Aot.DependencyInjection.Abstractions
{
    public class ServiceDescriptor
    {
        // ctors

        private ServiceDescriptor(Type serviceType, object? serviceKey, ServiceLifetime serviceLifetime)
        {
            ServiceLifetime = serviceLifetime;
            ServiceType = serviceType;
            ServiceKey = serviceKey;
        }

        public ServiceDescriptor(Type serviceType, object? serviceKey, ServiceLifetime serviceLifetime, Func<IServiceProvider, object> implementationFactory)
            : this(serviceType, serviceKey, serviceLifetime)
        {
            ImplementationFactory = implementationFactory;
        }

        public ServiceDescriptor(Type serviceType, object? serviceKey, object serviceInstance)
            : this(serviceType, serviceKey, ServiceLifetime.Singleton)
        {
            ServiceInstance = serviceInstance;
        }

        // props

        public Type ServiceType { get; }

        public object? ServiceKey { get; }

        public ServiceLifetime ServiceLifetime { get; }

        public Func<IServiceProvider, object>? ImplementationFactory { get; }

        public object? ServiceInstance { get; }

        // factories

        // singleton

        public static ServiceDescriptor Singleton(Type serviceType, object instance)
        {
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(instance, nameof(instance));

            return new(serviceType, null, instance);
        }

        public static ServiceDescriptor Singleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));
            
            return new(serviceType, null, ServiceLifetime.Singleton, implementationFactory);
        }

        public static ServiceDescriptor Singleton<TService>()
            where TService : class, IService<TService>
            => Singleton(typeof(TService), TService.New);

        public static ServiceDescriptor Singleton<TService>(TService instance)
            where TService : class
            => Singleton(typeof(TService), instance);

        public static ServiceDescriptor Singleton<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            => Singleton(typeof(TService), implementationFactory);

        public static ServiceDescriptor KeyedSingleton(Type serviceType, object serviceKey, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            return new(serviceType, serviceKey, ServiceLifetime.Singleton, implementationFactory);
        }

        public static ServiceDescriptor KeyedSingleton(Type serviceType, object serviceKey, object instance)
        {
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(instance, nameof(instance));

            return new(serviceType, serviceKey, instance);
        }

        public static ServiceDescriptor KeyedSingleton<TService>(object serviceKey)
            where TService : class, IService<TService>
            => KeyedSingleton(typeof(TService), serviceKey, TService.New);

        public static ServiceDescriptor KeyedSingleton<TService>(object serviceKey, TService instance)
            where TService : class
            => KeyedSingleton(typeof(TService), serviceKey, instance);

        public static ServiceDescriptor KeyedSingleton<TService>(object serviceKey, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            => KeyedSingleton(typeof(TService), serviceKey, implementationFactory);

        // scoped

        public static ServiceDescriptor Scoped(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));
            
            return new(serviceType, null, ServiceLifetime.Scoped, implementationFactory);
        }

        public static ServiceDescriptor Scoped<TService>()
            where TService : class, IService<TService>
            => Scoped(typeof(TService), TService.New);

        public static ServiceDescriptor Scoped<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            => Scoped(typeof(TService), implementationFactory);

        public static ServiceDescriptor KeyedScoped(Type serviceType, object serviceKey, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            return new(serviceType, serviceKey, ServiceLifetime.Scoped, implementationFactory);
        }

        public static ServiceDescriptor KeyedScoped<TService>(object serviceKey)
            where TService : class, IService<TService>
            => KeyedScoped(typeof(TService), serviceKey, TService.New);

        public static ServiceDescriptor KeyedScoped<TService>(object serviceKey, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            => KeyedScoped(typeof(TService), serviceKey, implementationFactory);

        // transiet

        public static ServiceDescriptor Transiet(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            return new(serviceType, null, ServiceLifetime.Transiet, implementationFactory);
        }

        public static ServiceDescriptor Transiet<TService>()
            where TService : class, IService<TService>
            => Transiet(typeof(TService), TService.New);

        public static ServiceDescriptor Transiet<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            => Transiet(typeof(TService), implementationFactory);

        public static ServiceDescriptor KeyedTransiet(Type serviceType, object serviceKey, Func<IServiceProvider, object> implementationFactory)
        {
            ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));
            ArgumentNullException.ThrowIfNull(serviceKey, nameof(serviceKey));
            ArgumentNullException.ThrowIfNull(implementationFactory, nameof(implementationFactory));

            return new(serviceType, serviceKey, ServiceLifetime.Transiet, implementationFactory);
        }

        public static ServiceDescriptor KeyedTransiet<TService>(object serviceKey)
            where TService : class, IService<TService>
            => KeyedTransiet(typeof(TService), serviceKey, TService.New);

        public static ServiceDescriptor KeyedTransiet<TService>(object serviceKey, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            => KeyedTransiet(typeof(TService), serviceKey, implementationFactory);
    }
}
