namespace Sudzekai.Hosting.Types.Exceptions
{
    public class ServiceNotFoundException(string serviceName) : Exception($"Service {serviceName} not registered");
}
