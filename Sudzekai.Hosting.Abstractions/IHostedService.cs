namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Defines a long-running background service whose lifetime is managed by the host.
    /// </summary>
    /// <remarks>
    /// A hosted service is started when the host runs and stopped when the host stops.
    /// It is resolved once and the same instance is reused for the whole host lifetime.
    /// </remarks>
    public interface IHostedService
    {
        /// <summary>
        /// Called when the host starts. Implement startup logic here.
        /// </summary>
        /// <returns>A task that completes when the service has started.</returns>
        Task RunAsync();

        /// <summary>
        /// Called when the host stops. Implement graceful shutdown logic here.
        /// </summary>
        /// <returns>A task that completes when the service has stopped.</returns>
        Task StopAsync();
    }
}