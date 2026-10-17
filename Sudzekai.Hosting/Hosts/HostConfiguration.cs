using Sudzekai.Hosting.Abstractions;

namespace Sudzekai.Hosting.Hosts
{
    internal class HostConfiguration : IConfiguration
    {
        public HostConfiguration(Dictionary<string, object?> configurationValues)
        {
            _configurationValues = configurationValues;
        }

        private Dictionary<string, object?> _configurationValues = [];

        public object? this[string key]
        {
            get
            {
                _configurationValues.TryGetValue(key, out var value);

                return value;
            }
        }
    }
}
