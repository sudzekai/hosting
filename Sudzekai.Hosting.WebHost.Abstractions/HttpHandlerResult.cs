using System.Net;

namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public record HttpHandlerResult(
        HttpStatusCode Code,
        object? Data = null
    );
}
