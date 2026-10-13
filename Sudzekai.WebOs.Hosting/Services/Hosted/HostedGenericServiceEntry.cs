namespace Sudzekai.Hosting.Services.Hosted
{
    internal sealed class HostedGenericServiceEntry<TService>(object key) : HostedServiceEntry(key)
        where TService : class, new()
    {
        private TService? _instance;

        public override object GetInstance()
        {
            _instance ??= new();

            return _instance;
        }
    }
}
