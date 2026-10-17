using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Builders;
using Sudzekai.Hosting.Builders.Configuration;
using Sudzekai.Hosting.Builders.ServiceCollection;

namespace Sudzekai.Hosting
{
    public sealed class HostBuilder : IHostBuilder
    {
        private HostBuilderServiceCollection _services = new();
        private ConfigurationBuilder _configurationBuilder = new();

        public IHostBuilderServiceCollection Services { get => _services; }

        public IConfigurationBuilder ConfigurationBuilder { get => _configurationBuilder; }

        public static IHostBuilder NewBuilder(params string[] args)
            => new HostBuilder();

        public IHost Build()
        {
            var configuration = _configurationBuilder.Build();

            Services.AddSingleton<IConfiguration>(configuration);

            return new Host(_services.Build());
        }
    }
}
