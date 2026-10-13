using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Hosts;
using Sudzekai.Hosting.Services.Abstractions;
using Sudzekai.Hosting.Types.Exceptions;

namespace Sudzekai.Hosting.Builders.ServiceCollection
{
    public partial class HostBuilderServiceCollection : IHostBuilderServiceCollection
    {
        private readonly ICollection<IServiceEntry> _services = [];

        private void ThrowIfRegistered(object key)
        {
            if (_services.FirstOrDefault(s => s.Key.Equals(key)) is not null)
                throw new ServiceAlreadyRegisteredException(key?.ToString() ?? "undefined");
        }

        internal HostServiceProvider Build()
            => new([.. _services]);
    }
}
