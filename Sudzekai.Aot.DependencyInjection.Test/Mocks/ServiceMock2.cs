using Sudzekai.Aot.DependencyInjection.Abstractions.src;

namespace Sudzekai.Aot.DependencyInjection.Test.Mocks
{
    internal class ServiceMock2 : IService<ServiceMock2>
    {
        public static ServiceMock2 New(IKeyedServiceProvider sp)
        {
            return new();
        }
    }
}
