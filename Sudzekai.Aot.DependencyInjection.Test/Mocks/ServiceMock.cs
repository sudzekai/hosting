using Sudzekai.Aot.DependencyInjection.Abstractions.src;

namespace Sudzekai.Aot.DependencyInjection.Test.Mocks
{
    internal class ServiceMock : IService<ServiceMock>
    {
        public static ServiceMock New(IKeyedServiceProvider sp)
        {
            return new();
        }
    }
}
