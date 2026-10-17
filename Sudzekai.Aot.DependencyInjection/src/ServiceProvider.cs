using Sudzekai.Aot.DependencyInjection.Abstractions;

namespace Sudzekai.Aot.DependencyInjection
{
    public class ServiceProvider : IKeyedServiceProvider
    {
        private readonly Dictionary<(object? ServiceKey, Type ServiceType), ServiceDescriptor> _serviceDescriptors = [];
        private readonly Dictionary<(object? ServiceKey, Type ServiceType), object> _singletonInstances = [];

        public ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors)
        {
            _serviceDescriptors = BuildServicesDictionary(serviceDescriptors);
        }

        private static Dictionary<(object? ServiceKey, Type ServiceType), ServiceDescriptor> BuildServicesDictionary(ICollection<ServiceDescriptor> serviceDescriptors)
        {
            Dictionary<(object? ServiceKey, Type ServiceType), ServiceDescriptor> result = [];

            foreach (var descriptor in serviceDescriptors)
            {
                result[(descriptor.ServiceKey, descriptor.ServiceType)] = descriptor;
            }

            return result;
        }

        public object? GetKeyedService(Type serviceType, object? serviceKey)
        {
            if (_serviceDescriptors.TryGetValue((serviceKey, serviceType), out var descriptor))
                return GetInstance(descriptor);

            return null;
        }

        public object GetRequiredKeyedService(Type serviceType, object? serviceKey)
            => GetKeyedService(serviceType, serviceKey)
            ?? throw new InvalidOperationException($"No service with key '{serviceKey}' and type '{serviceType}' registered");

        public object? GetService(Type serviceType)
        {
            if (_serviceDescriptors.TryGetValue((null, serviceType), out var descriptor))
                return GetInstance(descriptor);

            return null;
        }

        private object GetInstance(ServiceDescriptor descriptor)
        {
            if (descriptor.ServiceLifetime is ServiceLifetime.Singleton)
            {
                if (_singletonInstances.TryGetValue((descriptor.ServiceKey, descriptor.ServiceType), out var instance))
                    return instance;

                instance = CreateInstance(descriptor);

                ThrowIfInvalidType(descriptor, instance);

                _singletonInstances[(descriptor.ServiceKey, descriptor.ServiceType)] = instance;

                return instance;
            }

            if (descriptor.ServiceLifetime is ServiceLifetime.Scoped)
            {
                // пока без scoped логики

                var instance = CreateInstance(descriptor);

                ThrowIfInvalidType(descriptor, instance);

                return instance;
            }

            else
            {
                var instance = CreateInstance(descriptor);
                ThrowIfInvalidType(descriptor, instance);

                return instance;
            }
        }

        private object CreateInstance(ServiceDescriptor descriptor)
            => descriptor.ServiceInstance
               ?? descriptor.ImplementationFactory?.Invoke(this)
               ?? throw new InvalidOperationException($"Instance of service with key {descriptor.ServiceKey} and type {descriptor.ServiceType} was null when creating");

        private static void ThrowIfInvalidType(ServiceDescriptor descriptor, object instance)
        {
            if (!instance.GetType().IsAssignableTo(descriptor.ServiceType))
                throw new InvalidOperationException($"Service factory for service with key '{descriptor.ServiceKey}' and type '{descriptor.ServiceType}' returned an instance of type '{instance.GetType()}', which is not assignable to the registered service type.");
        }
    }
}
