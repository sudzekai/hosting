namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public interface IHandlersRegistry
    {
        IReadOnlyCollection<IHttpHandler> GetHandlers();

        IHandlersRegistry Register(IHttpHandler handler);
    }
}
