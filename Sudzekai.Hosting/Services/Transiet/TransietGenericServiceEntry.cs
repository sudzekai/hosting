namespace Sudzekai.Hosting.Services.Transiet
{
    internal class TransietGenericServiceEntry<TService>(object key) : TransietService(key)
        where TService : class, new()
    {
        public override object GetInstance()
            => new TService();
    }
}
