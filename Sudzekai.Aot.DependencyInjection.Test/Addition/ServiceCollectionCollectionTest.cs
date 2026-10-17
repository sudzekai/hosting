using Sudzekai.Aot.DependencyInjection.Abstractions.Extensions.ServiceDescriptors;
using Sudzekai.Aot.DependencyInjection.Abstractions.src;
using Sudzekai.Aot.DependencyInjection.Test.Mocks;

namespace Sudzekai.Aot.DependencyInjection.Test.Addition
{
    public class ServiceCollectionCollectionTest
    {
        [Fact]
        public void Addition_ReadOnly_Fail()
        {
            var services = new ServiceCollection();
            services.MakeReadOnly();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            Assert.Throws<InvalidOperationException>(() => services.Add(descriptor));

            Assert.Empty(services);
        }

        [Fact]
        public void Addition_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.Contains(descriptor, services);

            Assert.Single(services);
        }

        [Fact]
        public void Addition_NonKeyed_Keyed_SameTypes_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.KeyedSingleton<ServiceMock>("test");

            services.Add(descriptor);
            services.Add(descriptor1);

            Assert.Contains(descriptor, services);
            Assert.Contains(descriptor1, services);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Addition_DiffTypes_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);
            services.Add(descriptor1);

            Assert.Contains(descriptor, services);
            Assert.Contains(descriptor1, services);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Addition_SameTypes_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.Contains(descriptor, services);

            Assert.Throws<InvalidOperationException>(() => services.Add(descriptor1));

            Assert.Single(services);
        }

        [Fact]
        public void Addition_SameKeys_DiffTypes_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.KeyedSingleton<ServiceMock>("test");
            var descriptor1 = ServiceDescriptor.KeyedSingleton<ServiceMock1>("test");

            services.Add(descriptor);
            services.Add(descriptor1);

            Assert.Contains(descriptor, services);
            Assert.Contains(descriptor1, services);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Addition_DiffKeys_DiffTypes_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.KeyedSingleton<ServiceMock>("test");
            var descriptor1 = ServiceDescriptor.KeyedSingleton<ServiceMock1>("test_1");

            services.Add(descriptor);
            services.Add(descriptor1);

            Assert.Contains(descriptor, services);
            Assert.Contains(descriptor1, services);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Addition_DiffKeys_SameTypes_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.KeyedSingleton<ServiceMock>("test");
            var descriptor1 = ServiceDescriptor.KeyedSingleton<ServiceMock>("test_1");

            services.Add(descriptor);
            services.Add(descriptor1);

            Assert.Contains(descriptor, services);
            Assert.Contains(descriptor1, services);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Addition_SameKeys_SameTypes_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.KeyedSingleton<ServiceMock>("test");

            var descriptor1 = ServiceDescriptor.KeyedSingleton<ServiceMock>("test");

            services.Add(descriptor);

            Assert.Contains(descriptor, services);

            Assert.Throws<InvalidOperationException>(() => services.Add(descriptor1));

            Assert.Single(services);
        }

        [Fact]
        public void Indexer_Set_SameSlot_Diff_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            services[0] = descriptor1;

            Assert.DoesNotContain(descriptor, services);
            Assert.Equal(descriptor1, services[0]);
            Assert.Single(services);
        }

        [Fact]
        public void Indexer_Set_SameSlot_Duplicate_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            services[0] = descriptor1;

            Assert.DoesNotContain(descriptor, services);
            Assert.Equal(descriptor1, services[0]);
            Assert.Single(services);
        }

        [Fact]
        public void Indexer_Set_DiffSlot_Duplicate_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            var descriptor2 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);
            services.Add(descriptor1);

            Assert.Throws<InvalidOperationException>(() => services[1] = descriptor2);

            Assert.NotEqual(descriptor2, services[1]);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Indexer_Set_DiffSlot_DiffTypes_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            var descriptor2 = ServiceDescriptor.Singleton<ServiceMock2>();

            services.Add(descriptor);
            services.Add(descriptor1);

            services[1] = descriptor2;

            Assert.Equal(descriptor2, services[1]);
            Assert.DoesNotContain(descriptor1, services);

            Assert.Contains(descriptor, services);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Indexer_Set_ReadOnly_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            services.MakeReadOnly();

            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            Assert.Throws<InvalidOperationException>(() => services[0] = descriptor1);

            Assert.DoesNotContain(descriptor1, services);
            Assert.Equal(descriptor, services[0]);

            Assert.Single(services);
        }

        [Fact]
        public void Indexer_Set_OutOfRange_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            Assert.Throws<ArgumentOutOfRangeException>(() => services[99] = descriptor);

            Assert.DoesNotContain(descriptor, services);

            Assert.Empty(services);
        }

        [Fact]
        public void Indexer_Set_Duplicate_OutOfRange_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            Assert.Throws<ArgumentOutOfRangeException>(() => services[99] = descriptor1);

            Assert.DoesNotContain(descriptor1, services);

            Assert.Single(services);
        }

        [Fact]
        public void Indexer_Get_Equals_Setted_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            services[0] = descriptor1;

            Assert.Equal(descriptor1, services[0]);
        }

        [Fact]
        public void Indexer_Get_OutOfRange_Fail()
        {
            var services = new ServiceCollection();

            Assert.Throws<ArgumentOutOfRangeException>(() => services[99]);
        }

        [Fact]
        public void IndexOf_SameObject_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.Equal(0, services.IndexOf(descriptor));
        }

        [Fact]
        public void IndexOf_DiffRef_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.Equal(0, services.IndexOf(descriptor1));
        }

        [Fact]
        public void Insert_AtStart_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            services.Insert(0, descriptor1);

            Assert.Equal(descriptor1, services[0]);
            Assert.Equal(descriptor, services[1]);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Insert_AtMiddle_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();
            var descriptor2 = ServiceDescriptor.Singleton<ServiceMock2>();

            services.Add(descriptor);
            services.Add(descriptor1);

            services.Insert(1, descriptor2);

            Assert.Equal(descriptor, services[0]);
            Assert.Equal(descriptor2, services[1]);
            Assert.Equal(descriptor1, services[2]);

            Assert.Equal(3, services.Count);
        }

        [Fact]
        public void Insert_AtEnd_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            services.Insert(1, descriptor1);

            Assert.Equal(descriptor, services[0]);
            Assert.Equal(descriptor1, services[1]);

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public void Insert_Duplicate_SamePos_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.Throws<InvalidOperationException>(() => services.Insert(0, descriptor1));

            Assert.Equal(descriptor, services[0]);

            Assert.Single(services);
        }

        [Fact]
        public void Insert_Duplicate_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.Throws<InvalidOperationException>(() => services.Insert(1, descriptor));

            Assert.Equal(descriptor, services[0]);

            Assert.Single(services);
        }

        [Fact]
        public void Insert_Duplicate_OutOfRange_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.Throws<ArgumentOutOfRangeException>(() => services.Insert(99, descriptor1));

            Assert.Equal(descriptor, services[0]);

            Assert.Single(services);
        }

        [Fact]
        public void Insert_ReadOnly_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            services.MakeReadOnly();

            Assert.Throws<InvalidOperationException>(() => services.Insert(0, descriptor1));

            Assert.Equal(descriptor, services[0]);

            Assert.Single(services);
        }


        [Fact]
        public void Remove_SameObject_True()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.True(services.Remove(descriptor));

            Assert.Empty(services);
        }

        [Fact]
        public void Remove_DiffRef_True()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.True(services.Remove(descriptor1));

            Assert.Empty(services);
        }

        [Fact]
        public void Remove_NonExisting_False()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            Assert.False(services.Remove(descriptor));

            Assert.Empty(services);
        }

        [Fact]
        public void Remove_ReadOnly_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            services.Add(descriptor);

            services.MakeReadOnly();

            Assert.Throws<InvalidOperationException>(() => services.Remove(descriptor));

            Assert.Single(services);
        }

        [Fact]
        public void RemoveAt_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            services.RemoveAt(0);

            Assert.Empty(services);
        }

        [Fact]
        public void RemoveAt_OutOfRange_Fail()
        {
            var services = new ServiceCollection();

            Assert.Throws<ArgumentOutOfRangeException>(() => services.RemoveAt(0));
        }

        [Fact]
        public void RemoveAt_ReadOnly_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            services.MakeReadOnly();

            Assert.Throws<InvalidOperationException>(() => services.RemoveAt(0));

            Assert.Single(services);
        }

        [Fact]
        public void SetReadonly_Twice_Ok()
        {
            var services = new ServiceCollection();

            services.MakeReadOnly();
            services.MakeReadOnly();
        }

        [Fact]
        public void SetReadonly_Ok()
        {
            var services = new ServiceCollection();

            services.MakeReadOnly();

            Assert.True(services.IsReadOnly);
        }

        [Fact]
        public void Clear_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();
            var descriptor2 = ServiceDescriptor.Singleton<ServiceMock2>();

            services.Add(descriptor);
            services.Add(descriptor1);
            services.Add(descriptor2);

            services.Clear();

            Assert.Empty(services);
        }

        [Fact]
        public void Clear_Empty_Ok()
        {
            var services = new ServiceCollection();

            services.Clear();

            Assert.Empty(services);
        }

        [Fact]
        public void Clear_ReadOnly_Fail()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();
            var descriptor2 = ServiceDescriptor.Singleton<ServiceMock2>();

            services.Add(descriptor);
            services.Add(descriptor1);
            services.Add(descriptor2);

            services.MakeReadOnly();

            Assert.Throws<InvalidOperationException>(services.Clear);

            Assert.Equal(3, services.Count);
        }

        [Fact]
        public void Contains_NonExisting_False()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock1>();

            services.Add(descriptor);

            Assert.False(services.Contains(descriptor1));
        }

        [Fact]
        public void Contains_SameObject_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.True(services.Contains(descriptor));
        }

        [Fact]
        public void Contains_DiffRef_SameType_Ok()
        {
            var services = new ServiceCollection();

            var descriptor = ServiceDescriptor.Singleton<ServiceMock>();
            var descriptor1 = ServiceDescriptor.Singleton<ServiceMock>();

            services.Add(descriptor);

            Assert.True(services.Contains(descriptor1));
        }
    }
}
