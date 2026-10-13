namespace Sudzekai.Hosting.Services.Singleton
{
    internal class SingletonInstancedServiceEntry<TService>(object key, TService instance) : SingletonServiceEntry(key)
        where TService : class
    {
        private TService _instance = instance;

        public override object GetInstance()
            => _instance;
    }
}
