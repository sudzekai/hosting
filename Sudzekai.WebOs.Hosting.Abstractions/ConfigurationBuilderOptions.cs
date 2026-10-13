namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Settings used by <see cref="IConfigurationBuilder.Configure"/> to control
    /// how the host's <see cref="IConfiguration"/> is built.
    /// </summary>
    public class ConfigurationBuilderOptions
    {
        /// <summary>
        /// Gets or sets the type of the configuration provider to use.
        /// </summary>
        public ConfigurationProviderType ConfigurationProvider { get; set; } = ConfigurationProviderType.Json;

        /// <summary>
        /// Gets or sets the configuration source (for example, the file name) to load.
        /// </summary>
        public string ConfigurationSource { get; set; } = "appsettings.json";
    }
}