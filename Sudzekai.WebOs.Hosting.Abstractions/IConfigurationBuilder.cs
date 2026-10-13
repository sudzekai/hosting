namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Provides actions to configure how the host's <see cref="IConfiguration"/> is built.
    /// </summary>
    public interface IConfigurationBuilder
    {
        /// <summary>
        /// Configures the building of the host's <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="configure">
        /// An action that configures a <see cref="ConfigurationBuilderOptions"/> instance.
        /// </param>
        void Configure(Action<ConfigurationBuilderOptions> configure);
    }
}