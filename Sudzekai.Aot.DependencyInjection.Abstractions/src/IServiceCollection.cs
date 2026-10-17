using System.Collections;

namespace Sudzekai.Aot.DependencyInjection.Abstractions
{
    public interface IServiceCollection : IList<ServiceDescriptor>, ICollection<ServiceDescriptor>, IEnumerable<ServiceDescriptor>, IEnumerable;
}
