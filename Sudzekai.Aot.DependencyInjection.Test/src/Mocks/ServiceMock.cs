using Sudzekai.Aot.DependencyInjection.Abstractions;

namespace Sudzekai.Aot.DependencyInjection.Test.src.Mocks
{
    internal class ServiceMock : IService<ServiceMock>, IServiceMock
    {
        public static ServiceMock New(IServiceProvider sp)
        {
            return new();
        }
    }
}
