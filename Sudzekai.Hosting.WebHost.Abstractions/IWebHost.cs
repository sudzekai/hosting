namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public interface IWebHost
    {
        IHandlersRegistry Handlers { get; }

        int Port { get; }

        string Host { get; }
    }
}
