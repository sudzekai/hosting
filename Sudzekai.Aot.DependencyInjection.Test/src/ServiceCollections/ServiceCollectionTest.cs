using Sudzekai.Aot.DependencyInjection.Abstractions;
using Sudzekai.Aot.DependencyInjection.Test.src.Mocks;
using System.Collections;

namespace Sudzekai.Aot.DependencyInjection.Test.src.ServiceCollections
{
    public class ServiceCollectionTest
    {
        // Indexer — get

        [Fact]
        public void Indexer_Get_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            // act
            var result = services[0];

            // assert
            Assert.Equal(descriptor, result);
        }

        [Fact]
        public void Indexer_Get_OutOfRange_Fail()
        {
            // arrange
            ServiceCollection services = new();

            // act + assert
            Assert.Throws<ArgumentOutOfRangeException>(() => services[0]);
        }

        // Indexer — set

        [Fact]
        public void Indexer_Set_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();
            services.Add(descriptor);

            // act
            services[0] = descriptor1;

            // assert
            Assert.Equal(descriptor1, services[0]);
            Assert.Single(services);
        }

        [Fact]
        public void Indexer_Set_OutOfRange_Fail()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            // act + assert
            Assert.Throws<ArgumentOutOfRangeException>(() => services[0] = descriptor);
            Assert.Empty(services);
        }

        [Fact]
        public void Indexer_Set_ReadOnly_Fail()
        {
            // arrange
            ServiceCollection services = new();
            services.MakeReadOnly();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            // act + assert
            Assert.Throws<InvalidOperationException>(() => services[0] = descriptor);
            Assert.Empty(services);
        }

        // Add

        [Fact]
        public void Addition_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            // act
            services.Add(descriptor);

            // assert
            Assert.Contains(descriptor, services);
            Assert.Single(services);
        }

        [Fact]
        public void Addition_ReadOnly_Fail()
        {
            // arrange
            ServiceCollection services = new();
            services.MakeReadOnly();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            // act + assert
            Assert.Throws<InvalidOperationException>(() => services.Add(descriptor));
            Assert.Empty(services);
        }

        // Clear

        [Fact]
        public void Clear_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);

            // act
            services.Clear();

            // assert
            Assert.Empty(services);
        }

        [Fact]
        public void Clear_ReadOnly_Fail()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);

            services.MakeReadOnly();

            // act + assert
            Assert.Throws<InvalidOperationException>(services.Clear);

            Assert.Single(services);
        }

        // Contains

        [Fact]
        public void Contains_True()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);

            // act
            var result = services.Contains(descriptor);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void Contains_False()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            // act
            var result = services.Contains(descriptor1);

            // assert
            Assert.False(result);
        }

        // Count

        [Fact]
        public void Count_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);
            services.Add(descriptor1);

            // act
            var result = services.Count;

            // assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Count_Empty_Ok()
        {
            // arrange
            ServiceCollection services = new();

            // act
            var result = services.Count;

            // assert
            Assert.Equal(0, result);
        }

        // IsReadOnly / MakeReadOnly

        [Fact]
        public void IsReadOnly_False()
        {
            // arrange
            ServiceCollection services = new();

            // act
            var result = services.IsReadOnly;

            // assert
            Assert.False(result);
        }

        [Fact]
        public void IsReadOnly_True_AfterMakeReadOnly()
        {
            // arrange
            ServiceCollection services = new();
            services.MakeReadOnly();

            // act
            var result = services.IsReadOnly;

            // assert
            Assert.True(result);
        }

        // CopyTo

        [Fact]
        public void CopyTo_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);
            services.Add(descriptor1);

            var array = new ServiceDescriptor[2];

            // act
            services.CopyTo(array, 0);

            // assert
            Assert.Equal(descriptor, array[0]);
            Assert.Equal(descriptor1, array[1]);
        }

        [Fact]
        public void CopyTo_WithOffset_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);

            var array = new ServiceDescriptor[2];

            // act
            services.CopyTo(array, 1);

            // assert
            Assert.Null(array[0]);
            Assert.Equal(descriptor, array[1]);
        }

        // GetEnumerator

        [Fact]
        public void GetEnumerator_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);
            services.Add(descriptor1);

            // act
            var result = new List<ServiceDescriptor>();
            foreach (var item in services)
                result.Add(item);

            // assert
            Assert.Equal(2, result.Count);
            Assert.Equal(descriptor, result[0]);
            Assert.Equal(descriptor1, result[1]);
        }

        [Fact]
        public void GetEnumerator_Empty_Ok()
        {
            // arrange
            ServiceCollection services = new();

            // act + assert
            Assert.Empty(services);
        }

        [Fact]
        public void GetEnumerator_NonGeneric_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);

            // act
            var enumerator = ((IEnumerable)services).GetEnumerator();

            // assert
            Assert.True(enumerator.MoveNext());
            Assert.Equal(descriptor, enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        // IndexOf

        [Fact]
        public void IndexOf_Found_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);
            services.Add(descriptor1);

            // act
            var result = services.IndexOf(descriptor1);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void IndexOf_NotFound_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            // act
            var result = services.IndexOf(descriptor1);

            // assert
            Assert.Equal(-1, result);
        }

        // Insert

        [Fact]
        public void Insert_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            // act
            services.Insert(0, descriptor1);

            // assert
            Assert.Equal(2, services.Count);
            Assert.Equal(descriptor1, services[0]);
            Assert.Equal(descriptor, services[1]);
        }

        [Fact]
        public void Insert_OutOfRange_Fail()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            // act + assert
            Assert.Throws<ArgumentOutOfRangeException>(() => services.Insert(1, descriptor));
            Assert.Empty(services);
        }

        [Fact]
        public void Insert_ReadOnly_Fail()
        {
            // arrange
            ServiceCollection services = new();
            services.MakeReadOnly();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            // act + assert
            Assert.Throws<InvalidOperationException>(() => services.Insert(0, descriptor));
            Assert.Empty(services);
        }

        // Remove

        [Fact]
        public void Remove_Existing_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);
            services.Add(descriptor1);

            // act
            var result = services.Remove(descriptor);

            // assert
            Assert.True(result);
            Assert.Single(services);
            Assert.Equal(descriptor1, services[0]);
        }

        [Fact]
        public void Remove_NotExisting_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            // act
            var result = services.Remove(descriptor1);

            // assert
            Assert.False(result);
            Assert.Single(services);
        }

        [Fact]
        public void Remove_ReadOnly_Fail()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);
            services.MakeReadOnly();

            // act + assert
            Assert.Throws<InvalidOperationException>(() => services.Remove(descriptor));
            Assert.Single(services);
        }

        // RemoveAt

        [Fact]
        public void RemoveAt_Ok()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);
            services.Add(descriptor1);

            // act
            services.RemoveAt(0);

            // assert
            Assert.Single(services);
            Assert.Equal(descriptor1, services[0]);
        }

        [Fact]
        public void RemoveAt_OutOfRange_Fail()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);

            // act + assert
            Assert.Throws<ArgumentOutOfRangeException>(() => services.RemoveAt(1));
            Assert.Single(services);
        }

        [Fact]
        public void RemoveAt_ReadOnly_Fail()
        {
            // arrange
            ServiceCollection services = new();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);
            services.MakeReadOnly();

            // act + assert
            Assert.Throws<InvalidOperationException>(() => services.RemoveAt(0));
            Assert.Single(services);
        }
    }
}