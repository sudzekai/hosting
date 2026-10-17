namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder Configure(Action<WebHostOptions> configure);

        IWebHost Build();
    }
}
