namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Provides actions to configure and build an <see cref="IHost"/>.
    /// </summary>
    public interface IHostBuilder
    {
        /// <summary>
        /// Gets the collection used to register the host's services.
        /// </summary>
        IHostBuilderServiceCollection Services { get; }

        /// <summary>
        /// Gets the builder used to configure the host's <see cref="IConfiguration"/>.
        /// </summary>
        IConfigurationBuilder ConfigurationBuilder { get; }

        /// <summary>
        /// Builds the <see cref="IHost"/> from the configured services and configuration.
        /// </summary>
        /// <returns>The built <see cref="IHost"/> instance.</returns>
        IHost Build();
    }
}