using Sudzekai.Aot.DependencyInjection.Abstractions;

namespace Sudzekai.Aot.DependencyInjection.Test.src.Mocks
{
    internal class ServiceMock1 : IService<ServiceMock1>
    {
        public static ServiceMock1 New(IServiceProvider sp)
        {
            return new();
        }
    }
}
