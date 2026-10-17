using Sudzekai.Aot.DependencyInjection.Abstractions.src;

namespace Sudzekai.Aot.DependencyInjection.Test.Mocks
{
    internal class ServiceMock1 : IService<ServiceMock1>
    {
        public static ServiceMock1 New(IKeyedServiceProvider sp)
        {
            return new();
        }
    }
}
