using Sudzekai.Aot.DependencyInjection.Abstractions;
using Sudzekai.Aot.DependencyInjection.Test.src.Mocks;

namespace Sudzekai.Aot.DependencyInjection.Test.Addition.ServiceDescriptors
{
    public class TransietServiceDescriptorTest
    {
        [Fact]
        public void Creation_Ok()
        {
            // arrange
            var type = typeof(ServiceMock);
            var factory = ServiceMock.New;

            // act
            var descriptor = ServiceDescriptor.Transiet(type, factory);

            // assert
            Assert.Equal(type, descriptor.ServiceType);
            Assert.Equal(factory, descriptor.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, descriptor.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Transiet(type, factory));
        }

        [Fact]
        public void Creation_NullFactory_Fail()
        {
            // arrange
            var type = typeof(ServiceMock);
            Func<IServiceProvider, object> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Transiet(type, factory));
        }

        [Fact]
        public void Creation_Generic_Ok()
        {
            // act
            var descriptor = ServiceDescriptor.Transiet<ServiceMock>();

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(ServiceMock.New, descriptor.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceKey);
            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void Creation_Generic_Factory_Ok()
        {
            // arrange
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            var descriptor = ServiceDescriptor.Transiet<ServiceMock>(factory);

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(factory, descriptor.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceKey);
            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void Creation_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.Transiet<ServiceMock>(factory));
        }


        [Fact]
        public void KeyedCreation_Ok()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            var factory = ServiceMock.New;

            // act
            var descriptor = ServiceDescriptor.KeyedTransiet(type, serviceKey, factory);

            // assert
            Assert.Equal(type, descriptor.ServiceType);
            Assert.Equal(serviceKey, descriptor.ServiceKey);
            Assert.Equal(factory, descriptor.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Transiet, descriptor.ServiceLifetime);

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
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedTransiet(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedCreation_NullKey_Fail()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = null!;
            var factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedTransiet(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedCreation_NullFactory_Fail()
        {
            // arrange
            Type type = typeof(ServiceMock);
            object serviceKey = "test_service";
            Func<IServiceProvider, object> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedTransiet(type, serviceKey, factory));
        }

        [Fact]
        public void KeyedCreation_Generic_Ok()
        {
            // arrange
            object serviceKey = "test_service";

            // act
            var descriptor = ServiceDescriptor.KeyedTransiet<ServiceMock>(serviceKey);

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(serviceKey, descriptor.ServiceKey);
            Assert.Equal(ServiceMock.New, descriptor.ImplementationFactory);

            Assert.Equal(ServiceLifetime.Transiet, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void KeyedCreation_Generic_NullKey_Fail()
        {
            // arrange
            object serviceKey = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedTransiet<ServiceMock>(serviceKey));
        }

        [Fact]
        public void KeyedCreation_Generic_Factory_Ok()
        {
            // arrange
            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act
            var descriptor = ServiceDescriptor.KeyedTransiet<ServiceMock>(key, factory);

            // assert
            Assert.Equal(typeof(ServiceMock), descriptor.ServiceType);
            Assert.Equal(key, descriptor.ServiceKey);
            Assert.Equal(factory, descriptor.ImplementationFactory);
            Assert.Equal(ServiceLifetime.Transiet, descriptor.ServiceLifetime);

            Assert.Null(descriptor.ServiceInstance);
        }

        [Fact]
        public void KeyedCreation_Generic_Factory_NullKey_Fail()
        {
            // arrange
            object key = null!;
            Func<IServiceProvider, ServiceMock> factory = ServiceMock.New;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedTransiet<ServiceMock>(key, factory));
        }

        [Fact]
        public void KeyedCreation_Generic_Factory_NullFactory_Fail()
        {
            // arrange
            object key = "test_service";
            Func<IServiceProvider, ServiceMock> factory = null!;

            // act + assert
            Assert.Throws<ArgumentNullException>(() => ServiceDescriptor.KeyedTransiet<ServiceMock>(key, factory));
        }
    }
}
