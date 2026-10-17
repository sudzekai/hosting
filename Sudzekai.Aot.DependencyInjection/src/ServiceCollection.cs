using Sudzekai.Aot.DependencyInjection.Abstractions;
using System.Collections;

namespace Sudzekai.Aot.DependencyInjection
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly List<ServiceDescriptor> _serviceDescriptors = [];
        private bool _isReadOnly = false;

        public ServiceDescriptor this[int index]
        {
            get => _serviceDescriptors[index];
            set
            {
                ThrowIfReadOnly();

                _serviceDescriptors[index] = value;
            }
        }

        public int Count => _serviceDescriptors.Count;

        public bool IsReadOnly => _isReadOnly;

        public void Add(ServiceDescriptor item)
        {
            ThrowIfReadOnly();

            _serviceDescriptors.Add(item);
        }

        public void Clear()
        {
            ThrowIfReadOnly();

            _serviceDescriptors.Clear();
        }

        public bool Contains(ServiceDescriptor item)
            => _serviceDescriptors.Contains(item);

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
            => _serviceDescriptors.CopyTo(array, arrayIndex);

        public IEnumerator<ServiceDescriptor> GetEnumerator()
            => _serviceDescriptors.GetEnumerator();

        public int IndexOf(ServiceDescriptor item)
            => _serviceDescriptors.IndexOf(item);

        public void Insert(int index, ServiceDescriptor item)
        {
            ThrowIfReadOnly();

            _serviceDescriptors.Insert(index, item);
        }

        public bool Remove(ServiceDescriptor item)
        {
            ThrowIfReadOnly();

            return _serviceDescriptors.Remove(item);
        }

        public void RemoveAt(int index)
        {
            ThrowIfReadOnly();

            _serviceDescriptors.RemoveAt(index);
        }

        public void MakeReadOnly()
            => _isReadOnly = true;

        private void ThrowIfReadOnly()
        {
            if (IsReadOnly)
                throw new InvalidOperationException("Service collection is read only");
        }

        IEnumerator IEnumerable.GetEnumerator()
            => _serviceDescriptors.GetEnumerator();
    }
}
