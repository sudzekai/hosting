using Sudzekai.Aot.DependencyInjection.Abstractions;
using Sudzekai.Aot.DependencyInjection.Test.src.Mocks;

namespace Sudzekai.Aot.DependencyInjection.Test.src.ServiceCollections.Extensions
{
    public class ServiceCollectionSingletonDescriptorsTest
    {
        // AddSingleton(Type, Func) — non-generic, factory

        [Fact]
        public void Addition_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            var type = typeof(ServiceMock);
            var factory = ServiceMock.New;

            // act
            services.AddSingleton(type, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(type, created.ServiceType);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void Addition_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            Type type = typeof(ServiceMock);
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton(type, factory));
        }

        [Fact]
        public void Addition_NullType_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = null!;
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton(type, factory));
        }

        [Fact]
        public void Addition_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            var type = typeof(ServiceMock);
            Func<IServiceProvider, object> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton(type, factory));
        }

        // AddSingleton<T>() — generic, no args

        [Fact]
        public void Addition_Generic_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            // act
            services.AddSingleton<ServiceMock>();

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void Addition_Generic_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ServiceMock>());
        }

        // AddSingleton<T>(Func) — generic, factory

        [Fact]
        public void Addition_Generic_Factory_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            services.AddSingleton<ServiceMock>(factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void Addition_Generic_Factory_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ServiceMock>(factory));
        }

        [Fact]
        public void Addition_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ServiceMock>(factory));
        }

        // AddSingleton<TService, TImplementation>() — two types

        [Fact]
        public void Addition_Generic_TwoTypes_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            // act
            services.AddSingleton<IServiceMock, ServiceMock>();

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(IServiceMock), created.ServiceType);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void Addition_Generic_TwoTypes_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton<IServiceMock, ServiceMock>());
        }

        // AddKeyedSingleton(Type, key, Func) — non-generic keyed factory

        [Fact]
        public void KeyedAddition_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            var factory = ServiceMock.New;

            // act
            services.AddKeyedSingleton(type, serviceKey, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(type, created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(factory, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void KeyedAddition_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedAddition_NullType_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = null!;
            object serviceKey = "test_service";
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedAddition_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            object serviceKey = null!;
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedAddition_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            Func<IServiceProvider, object> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton(type, serviceKey, factory));
        }

        // AddKeyedSingleton<T>(key) — generic keyed, no factory

        [Fact]
        public void KeyedAddition_Generic_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = "test_service";

            // act
            services.AddKeyedSingleton<ServiceMock>(serviceKey);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void KeyedAddition_Generic_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            object serviceKey = "test_service";

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<ServiceMock>(serviceKey));
        }

        [Fact]
        public void KeyedAddition_Generic_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<ServiceMock>(serviceKey));
        }

        // AddKeyedSingleton<T>(key, Func) — generic keyed factory

        [Fact]
        public void KeyedAddition_Generic_Factory_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            services.AddKeyedSingleton<ServiceMock>(key, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(key, created.ServiceKey);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void KeyedAddition_Generic_Factory_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<ServiceMock>(key, factory));
        }

        [Fact]
        public void KeyedAddition_Generic_Factory_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object key = null!;
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<ServiceMock>(key, factory));
        }

        [Fact]
        public void KeyedAddition_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<ServiceMock>(key, factory));
        }

        // AddKeyedSingleton<TService, TImplementation>(key) — two types, keyed

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = "test_service";

            // act
            services.AddKeyedSingleton<IServiceMock, ServiceMock>(serviceKey);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(IServiceMock), created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            object serviceKey = "test_service";

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<IServiceMock, ServiceMock>(serviceKey));
        }

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<IServiceMock, ServiceMock>(serviceKey));
        }
        // AddKeyedSingleton(Type, key, object) — non-generic, instanced

        [Fact]
        public void KeyedAddition_Instanced_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            ServiceMock instance = new();

            // act
            services.AddKeyedSingleton(type, serviceKey, instance);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(type, created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(instance, created.ServiceInstance);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ImplementationFactory);
        }

        [Fact]
        public void KeyedAddition_Instanced_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton(type, serviceKey, instance));
        }

        [Fact]
        public void KeyedAddition_Instanced_NullType_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = null!;
            object serviceKey = "test_service";
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton(type, serviceKey, instance));
        }

        [Fact]
        public void KeyedAddition_Instanced_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            object serviceKey = null!;
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton(type, serviceKey, instance));
        }

        [Fact]
        public void KeyedAddition_Instanced_NullInstance_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            ServiceMock instance = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton(type, serviceKey, instance));
        }


        // AddKeyedSingleton<T>(key, T) — generic, instanced

        [Fact]
        public void KeyedAddition_Generic_Instanced_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = "test_service";
            ServiceMock instance = new();

            // act
            services.AddKeyedSingleton<ServiceMock>(serviceKey, instance);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(instance, created.ServiceInstance);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ImplementationFactory);
        }

        [Fact]
        public void KeyedAddition_Generic_Instanced_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            object serviceKey = "test_service";
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<ServiceMock>(serviceKey, instance));
        }

        [Fact]
        public void KeyedAddition_Generic_Instanced_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = null!;
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<ServiceMock>(serviceKey, instance));
        }

        [Fact]
        public void KeyedAddition_Generic_Instanced_NullInstance_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = "test_service";
            ServiceMock instance = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedSingleton<ServiceMock>(serviceKey, instance));
        }

        // AddSingleton(Type, object) — non-generic, instanced

        [Fact]
        public void Addition_Instanced_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            ServiceMock instance = new();

            // act
            services.AddSingleton(type, instance);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(type, created.ServiceType);
            Assert.Equal(instance, created.ServiceInstance);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ImplementationFactory);
        }

        [Fact]
        public void Addition_Instanced_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            Type type = typeof(ServiceMock);
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton(type, instance));
        }

        [Fact]
        public void Addition_Instanced_NullType_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = null!;
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton(type, instance));
        }

        [Fact]
        public void Addition_Instanced_NullInstance_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            ServiceMock instance = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton(type, instance));
        }


        // AddSingleton<T>(T) — generic, instanced

        [Fact]
        public void Addition_Generic_Instanced_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            ServiceMock instance = new();

            // act
            services.AddSingleton<ServiceMock>(instance);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(instance, created.ServiceInstance);
            Assert.Equal(ServiceLifetime.Singleton, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ImplementationFactory);
        }

        [Fact]
        public void Addition_Generic_Instanced_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ServiceMock>(instance));
        }

        [Fact]
        public void Addition_Generic_Instanced_NullInstance_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            ServiceMock instance = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ServiceMock>(instance));
        }
    }
}