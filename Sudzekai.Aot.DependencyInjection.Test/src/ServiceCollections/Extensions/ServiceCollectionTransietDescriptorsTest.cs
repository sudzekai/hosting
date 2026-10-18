using Sudzekai.Aot.DependencyInjection.Abstractions;
using Sudzekai.Aot.DependencyInjection.Test.src.Mocks;

namespace Sudzekai.Aot.DependencyInjection.Test.src.ServiceCollections.Extensions
{
    public class ServiceCollectionTransietDescriptorsTest
    {
        // AddTransiet(Type, Func) — non-generic, factory

        [Fact]
        public void Addition_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            var type = typeof(ServiceMock);
            var factory = ServiceMock.New;

            // act
            services.AddTransiet(type, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(type, created.ServiceType);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, created.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => services.AddTransiet(type, factory));
        }

        [Fact]
        public void Addition_NullType_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = null!;
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddTransiet(type, factory));
        }

        [Fact]
        public void Addition_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            var type = typeof(ServiceMock);
            Func<IServiceProvider, object> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddTransiet(type, factory));
        }

        // AddTransiet<T>() — generic, no args

        [Fact]
        public void Addition_Generic_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            // act
            services.AddTransiet<ServiceMock>();

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void Addition_Generic_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddTransiet<ServiceMock>());
        }

        // AddTransiet<T>(Func) — generic, factory

        [Fact]
        public void Addition_Generic_Factory_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            services.AddTransiet<ServiceMock>(factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, created.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => services.AddTransiet<ServiceMock>(factory));
        }

        [Fact]
        public void Addition_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddTransiet<ServiceMock>(factory));
        }

        // AddTransiet<TService, TImplementation>() — two types

        [Fact]
        public void Addition_Generic_TwoTypes_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            // act
            services.AddTransiet<IServiceMock, ServiceMock>();

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(IServiceMock), created.ServiceType);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void Addition_Generic_TwoTypes_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddTransiet<IServiceMock, ServiceMock>());
        }

        // AddKeyedTransiet(Type, key, Func) — non-generic keyed factory

        [Fact]
        public void KeyedAddition_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            var factory = ServiceMock.New;

            // act
            services.AddKeyedTransiet(type, serviceKey, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(type, created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(factory, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Transiet, created.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet(type, serviceKey, factory));
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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet(type, serviceKey, factory));
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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet(type, serviceKey, factory));
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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet(type, serviceKey, factory));
        }

        // AddKeyedTransiet<T>(key) — generic keyed, no factory

        [Fact]
        public void KeyedAddition_Generic_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = "test_service";

            // act
            services.AddKeyedTransiet<ServiceMock>(serviceKey);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Transiet, created.ServiceLifetime);

            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void KeyedAddition_Generic_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            object serviceKey = "test_service";

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet<ServiceMock>(serviceKey));
        }

        [Fact]
        public void KeyedAddition_Generic_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet<ServiceMock>(serviceKey));
        }

        // AddKeyedTransiet<T>(key, Func) — generic keyed factory

        [Fact]
        public void KeyedAddition_Generic_Factory_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            services.AddKeyedTransiet<ServiceMock>(key, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(key, created.ServiceKey);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, created.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet<ServiceMock>(key, factory));
        }

        [Fact]
        public void KeyedAddition_Generic_Factory_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object key = null!;
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet<ServiceMock>(key, factory));
        }

        [Fact]
        public void KeyedAddition_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet<ServiceMock>(key, factory));
        }

        // AddKeyedTransiet<TService, TImplementation>(key) — two types, keyed

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = "test_service";

            // act
            services.AddKeyedTransiet<IServiceMock, ServiceMock>(serviceKey);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(IServiceMock), created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Transiet, created.ServiceLifetime);

            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            object serviceKey = "test_service";

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet<IServiceMock, ServiceMock>(serviceKey));
        }

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedTransiet<IServiceMock, ServiceMock>(serviceKey));
        }
    }
}