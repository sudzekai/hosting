namespace Sudzekai.Hosting.WebHost.Abstractions
{
    public sealed class WebHostOptions
    {
        public IResultFilter? ResultFilter { get; }
        public IExceptionFilter? ExceptionFilter { get; }
    }
}
