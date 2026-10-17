using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.WebHost.Abstractions;
using System.Net;

namespace Sudzekai.Hosting.WebHost.WebHosts
{
    internal class HandlersRegistry : IHandlersRegistry
    {
        public IReadOnlyCollection<IHttpHandler> GetHandlers()
        {
            throw new NotImplementedException();
        }

        public IHandlersRegistry MapDelete(IHttpHandler handler)
        {
            throw new NotImplementedException();
        }

        public IHandlersRegistry MapGet<THandler>(string pattern, Func<IHostServiceProvider, HttpListenerContext, HttpHandlerResult> handlerFunc)
        {
            throw new NotImplementedException();
        }

        public IHandlersRegistry MapPatch(IHttpHandler handler)
        {
            throw new NotImplementedException();
        }

        public IHandlersRegistry MapPost(IHttpHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
