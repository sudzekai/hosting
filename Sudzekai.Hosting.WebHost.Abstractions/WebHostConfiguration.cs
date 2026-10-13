namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public sealed class WebHostConfiguration
    {
        public IResultFilter? ResultFilter { get; }
        public IExceptionFilter? ExceptionFilter { get; }
    }
}
