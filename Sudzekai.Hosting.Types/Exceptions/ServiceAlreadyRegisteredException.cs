namespace Sudzekai.Hosting.Types.Exceptions
{
    public class ServiceAlreadyRegisteredException(string serviceName) : Exception($"Service with same key: {serviceName} already registered");
}
