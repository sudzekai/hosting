namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Provides actions to control the lifetime of the process (host).
    /// </summary>
    /// <remarks>
    /// <see cref="RunAsync"/> starts the host and its hosted services;
    /// <see cref="StopAsync"/> gracefully stops them. The host also owns the
    /// service provider and disposes the registered disposable services when it is disposed.
    /// </remarks>
    public interface IHost : IAsyncDisposable
    {
        /// <summary>
        /// Gets the host's configuration.
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the host's service provider.
        /// </summary>
        IHostServiceProvider Services { get; }

        /// <summary>
        /// Runs the process by starting all registered hosted services.
        /// </summary>
        /// <returns>A task that completes when the hosted services have started.</returns>
        Task RunAsync();

        /// <summary>
        /// Stops the process by stopping all registered hosted services.
        /// </summary>
        /// <returns>A task that completes when the hosted services have stopped.</returns>
        Task StopAsync();
    }
}