using Sudzekai.Hosting.WebHost.Abstractions;

namespace Sudzekai.Hosting.WebHost
{
    internal class WebHost : IWebHost
    {
        public IHandlersRegistry Handlers { get; }
    }
}
