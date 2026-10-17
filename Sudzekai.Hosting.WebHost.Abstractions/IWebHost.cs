namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public interface IWebHost
    {
        IHandlersRegistry Handlers { get; }
    }
}
