namespace Sudzekai.Hosting.Services.Singleton
{
    internal class SingletonGenericServiceEntry<TService>(object key) : SingletonServiceEntry(key)
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
