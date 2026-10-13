using System.Text.RegularExpressions;

namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public interface IHttpHandler
    {
        Regex Endpoint { get; }

        HttpMethod Method { get; }

        Task HandleAsync();
    }
}
