using Sudzekai.Aot.DependencyInjection.Abstractions;
using Sudzekai.Aot.DependencyInjection.Test.src.Mocks;

namespace Sudzekai.Aot.DependencyInjection.Test.src.ServiceCollections.Extensions
{
    public class ServiceCollectionScopedDescriptorsTest
    {
        // AddScoped(Type, Func) — non-generic, factory

        [Fact]
        public void Addition_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            var type = typeof(ServiceMock);
            var factory = ServiceMock.New;

            // act
            services.AddScoped(type, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(type, created.ServiceType);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Scoped, created.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => services.AddScoped(type, factory));
        }

        [Fact]
        public void Addition_NullType_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = null!;
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddScoped(type, factory));
        }

        [Fact]
        public void Addition_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            var type = typeof(ServiceMock);
            Func<IServiceProvider, object> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddScoped(type, factory));
        }

        // AddScoped<T>() — generic, no args

        [Fact]
        public void Addition_Generic_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            // act
            services.AddScoped<ServiceMock>();

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Scoped, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void Addition_Generic_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddScoped<ServiceMock>());
        }

        // AddScoped<T>(Func) — generic, factory

        [Fact]
        public void Addition_Generic_Factory_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            services.AddScoped<ServiceMock>(factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Scoped, created.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => services.AddScoped<ServiceMock>(factory));
        }

        [Fact]
        public void Addition_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddScoped<ServiceMock>(factory));
        }

        // AddScoped<TService, TImplementation>() — two types

        [Fact]
        public void Addition_Generic_TwoTypes_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            // act
            services.AddScoped<IServiceMock, ServiceMock>();

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(IServiceMock), created.ServiceType);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Scoped, created.ServiceLifetime);

            Assert.Null(created.ServiceKey);
            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void Addition_Generic_TwoTypes_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddScoped<IServiceMock, ServiceMock>());
        }

        // AddKeyedScoped(Type, key, Func) — non-generic keyed factory

        [Fact]
        public void KeyedAddition_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            var factory = ServiceMock.New;

            // act
            services.AddKeyedScoped(type, serviceKey, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(type, created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(factory, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Scoped, created.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped(type, serviceKey, factory));
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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped(type, serviceKey, factory));
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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped(type, serviceKey, factory));
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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped(type, serviceKey, factory));
        }

        // AddKeyedScoped<T>(key) — generic keyed, no factory

        [Fact]
        public void KeyedAddition_Generic_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = "test_service";

            // act
            services.AddKeyedScoped<ServiceMock>(serviceKey);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Scoped, created.ServiceLifetime);

            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void KeyedAddition_Generic_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            object serviceKey = "test_service";

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped<ServiceMock>(serviceKey));
        }

        [Fact]
        public void KeyedAddition_Generic_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped<ServiceMock>(serviceKey));
        }

        // AddKeyedScoped<T>(key, Func) — generic keyed factory

        [Fact]
        public void KeyedAddition_Generic_Factory_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            services.AddKeyedScoped<ServiceMock>(key, factory);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(ServiceMock), created.ServiceType);
            Assert.Equal(key, created.ServiceKey);
            Assert.Equal(factory, created.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Scoped, created.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped<ServiceMock>(key, factory));
        }

        [Fact]
        public void KeyedAddition_Generic_Factory_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object key = null!;
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped<ServiceMock>(key, factory));
        }

        [Fact]
        public void KeyedAddition_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped<ServiceMock>(key, factory));
        }

        // AddKeyedScoped<TService, TImplementation>(key) — two types, keyed

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_Ok()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = "test_service";

            // act
            services.AddKeyedScoped<IServiceMock, ServiceMock>(serviceKey);

            Assert.Single(services);
            var created = services[0];

            // assert
            Assert.Equal(typeof(IServiceMock), created.ServiceType);
            Assert.Equal(serviceKey, created.ServiceKey);
            Assert.Equal(ServiceMock.New, created.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Scoped, created.ServiceLifetime);

            Assert.Null(created.ServiceInstance);
        }

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_NullCollection_Fail()
        {
            // arrange
            ServiceCollection services = null!;

            object serviceKey = "test_service";

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped<IServiceMock, ServiceMock>(serviceKey));
        }

        [Fact]
        public void KeyedAddition_Generic_TwoTypes_NullKey_Fail()
        {
            // arrange
            var services = new ServiceCollection();

            object serviceKey = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => services.AddKeyedScoped<IServiceMock, ServiceMock>(serviceKey));
        }
    }
}