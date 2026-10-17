using System.Text.RegularExpressions;

namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public interface IHttpHandler
    {
        Task HandleAsync();
    }
}
