using Sudzekai.Aot.DependencyInjection.Abstractions;

namespace Sudzekai.Aot.DependencyInjection.Test.src.Mocks
{
    internal class ServiceMock2 : IService<ServiceMock2>
    {
        public static ServiceMock2 New(IServiceProvider sp)
        {
            return new();
        }
    }
}
