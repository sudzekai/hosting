namespace Sudzekai.Hosting.Services.Hosted
{
    internal sealed class HostedInstancedServiceEntry<TService>(object key, TService instance) : HostedServiceEntry(key)
        where TService : class
    {
        private readonly TService _instance = instance;

        public override object GetInstance()
            => _instance;
    }
}
