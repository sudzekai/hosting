using Sudzekai.Hosting.WebHost.Abstractions;

namespace Sudzekai.Hosting.WebHost
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private bool _configured;

        public static IWebHostBuilder Create()
            => new WebHostBuilder();

        IWebHostBuilder IWebHostBuilder.Configure(Action<WebHostOptions> configure)
        {
            return this;
        }

        IWebHost IWebHostBuilder.Build()
        {
            throw new NotImplementedException();
        }
    }
}
