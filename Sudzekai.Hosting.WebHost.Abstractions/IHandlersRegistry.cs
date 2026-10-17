using Sudzekai.Hosting.Abstractions;
using System.Net;

namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public interface IHandlersRegistry
    {
        IReadOnlyCollection<IHttpHandler> GetHandlers();

        IHandlersRegistry MapGet<THandler>(string pattern, Func<IHostServiceProvider, HttpListenerContext, HttpHandlerResult> handlerFunc);
        IHandlersRegistry MapPost(IHttpHandler handler);
        IHandlersRegistry MapPatch(IHttpHandler handler);
        IHandlersRegistry MapDelete(IHttpHandler handler);
    }
}
