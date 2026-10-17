namespace Sudzekai.Hosting.Builders.Configuration.Provides
{
    internal interface IConfigurationProvider
    {
        Dictionary<string, object?> LoadConfigurationValues();
    }
}
