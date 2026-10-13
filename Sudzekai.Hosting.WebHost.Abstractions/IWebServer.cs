namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public interface IWebServer
    {
        IWebHost WebHost { get; }

        void StartListening();

        void StopListening();
    }
}
