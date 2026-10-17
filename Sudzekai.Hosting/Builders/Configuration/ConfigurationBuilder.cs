using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Builders.Configuration.Provides;
using Sudzekai.Hosting.Hosts;

namespace Sudzekai.Hosting.Builders.Configuration
{
    internal class ConfigurationBuilder : IConfigurationBuilder
    {
        private IConfigurationProvider _provider = new JsonConfigurationProvider("appsettings.json");

        public IConfiguration Build()
        {
            return new HostConfiguration(_provider.LoadConfigurationValues());
        }

        public void Configure(Action<ConfigurationBuilderOptions> configure)
        {
            ConfigurationBuilderOptions o = new();

            configure(o);

            _provider = o.ConfigurationProvider switch
            {
                ConfigurationProviderType.Json => new JsonConfigurationProvider(o.ConfigurationSource),
                _ => throw new InvalidOperationException("Unknown configuration provider type was provided")
            };
        }
    }
}
