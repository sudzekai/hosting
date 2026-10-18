using Sudzekai.Aot.DependencyInjection.Abstractions;
using Sudzekai.Aot.DependencyInjection.Test.src.Mocks;

namespace Sudzekai.Aot.DependencyInjection.Test.Addition.ServiceDescriptors
{
    public class SingletonServiceDescriptorTest
    {
        [Fact]
        public void Creation_Ok()
        {
            // arrange
            var type = typeof(ServiceMock);
            var factory = ServiceMock.New;

            // act
            var descriptor = ServiceDescriptor.Singleton(type, factory);

            // assert
            Assert.Equal(type, descriptor.ServiceType);
            Assert.Equal(factory, descriptor.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceKey);
            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void Creation_NullType_Fail()
        {
            // arrange
            Type type = null!;
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Singleton(type, factory));
        }

        [Fact]
        public void Creation_NullFactory_Fail()
        {
            // arrange
            var type = typeof(ServiceMock);
            Func<IServiceProvider, object> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Singleton(type, factory));
        }

        [Fact]
        public void Creation_Instanced_Ok()
        {
            // arrange
            var type = typeof(ServiceMock);
            var instance = new ServiceMock();

            // act
            var descriptor = ServiceDescriptor.Singleton(type, instance);

            // assert
            Assert.Equal(type, descriptor.ServiceType);
            Assert.Equal(instance, descriptor.ServiceInstance);
            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceKey);
            Assert.Null(descriptor.ImplementationFactory);
        }

        [Fact]
        public void Creation_Instanced_NullType_Fail()
        {
            // arrange
            Type type = null!;
            object instance = new ServiceMock();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Singleton(type, instance));
        }

        [Fact]
        public void Creation_Instanced_NullInstance_Fail()
        {
            // arrange
            var type = typeof(ServiceMock);
            object instance = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Singleton(type, instance));
        }

        [Fact]
        public void Creation_Generic_Ok()
        {
            // act
            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(ServiceMock.New, descriptor.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceKey);
            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void Creation_Generic_Instanced_Ok()
        {
            // arrange
            var instance = new ServiceMock();

            // act
            var descriptor = ServiceDescriptor.Singleton<ServiceMock>(instance);

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(instance, descriptor.ServiceInstance);
            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceKey);
            Assert.Null(descriptor.ImplementationFactory);
        }

        [Fact]
        public void Creation_Generic_Instanced_NullInstane_Fail()
        {
            // arrange
            ServiceMock instance = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Singleton<ServiceMock>(instance));
        }

        [Fact]
        public void Creation_Generic_Factory_Ok()
        {
            // arrange
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            var descriptor = ServiceDescriptor.Singleton<ServiceMock>(factory);

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(factory, descriptor.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceKey);
            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void Creation_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Singleton<ServiceMock>(factory));
        }

        [Fact]
        public void KeyedCreation_Ok()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            var factory = ServiceMock.New;

            // act
            var descriptor = ServiceDescriptor.KeyedSingleton(type, serviceKey, factory);

            // assert
            Assert.Equal(type, descriptor.ServiceType);
            Assert.Equal(serviceKey, descriptor.ServiceKey);
            Assert.Equal(factory, descriptor.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void KeyedCreation_NullType_Fail()
        {
            // arrange
            Type type = null!;
            object serviceKey = "test_service";
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedCreation_NullKey_Fail()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = null!;
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedCreation_NullFactory_Fail()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            Func<IServiceProvider, object> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedCreation_Instanced_Ok()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            ServiceMock instance = new();

            // act
            var descriptor = ServiceDescriptor.KeyedSingleton(type, serviceKey, instance);

            // assert
            Assert.Equal(type, descriptor.ServiceType);
            Assert.Equal(serviceKey, descriptor.ServiceKey);
            Assert.Equal(instance, descriptor.ServiceInstance);

            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ImplementationFactory);
        }

        [Fact]
        public void KeyedCreation_Instanced_NullType_Fail()
        {
            // arrange
            Type type = null!;
            object serviceKey = "test_service";
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton(type, serviceKey, instance));
        }

        [Fact]
        public void KeyedCreation_Instanced_NullKey_Fail()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = null!;
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton(type, serviceKey, instance));
        }

        [Fact]
        public void KeyedCreation_Instanced_NullInstance_Fail()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            ServiceMock instance = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton(type, serviceKey, instance));
        }

        [Fact]
        public void KeyedCreation_Generic_Ok()
        {
            // arrange
            object serviceKey = "test_service";

            // act
            var descriptor = ServiceDescriptor.KeyedSingleton<ServiceMock>(serviceKey);

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(serviceKey, descriptor.ServiceKey);
            Assert.Equal(ServiceMock.New, descriptor.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void KeyedCreation_Generic_NullKey_Fail()
        {
            // arrange
            object serviceKey = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton<ServiceMock>(serviceKey));
        }

        [Fact]
        public void KeyedCreation_Generic_Instanced_Ok()
        {
            // arrange
            object serviceKey = "test_service";
            ServiceMock instance = new();

            // act
            var descriptor = ServiceDescriptor.KeyedSingleton<ServiceMock>(serviceKey, instance);

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(serviceKey, descriptor.ServiceKey);
            Assert.Equal(instance, descriptor.ServiceInstance);

            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ImplementationFactory);
        }

        [Fact]
        public void KeyedCreation_Generic_Instanced_NullKey_Fail()
        {
            // arrange
            object serviceKey = null!;
            ServiceMock instance = new();

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton<ServiceMock>(serviceKey, instance));
        }

        [Fact]
        public void KeyedCreation_Generic_Instanced_NullInstance_Fail()
        {
            // arrange
            object serviceKey = "test_service";
            ServiceMock instance = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton<ServiceMock>(serviceKey, instance));
        }

        [Fact]
        public void KeyedCreation_Generic_Factory_Ok()
        {
            // arrange
            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            var descriptor = ServiceDescriptor.KeyedSingleton<ServiceMock>(key, factory);

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(key, descriptor.ServiceKey);
            Assert.Equal(factory, descriptor.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Singleton, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void KeyedCreation_Generic_Factory_NullKey_Fail()
        {
            // arrange
            object key = null!;
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton<ServiceMock>(key, factory));
        }

        [Fact]
        public void KeyedCreation_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedSingleton<ServiceMock>(key, factory));
        }
    }
}
