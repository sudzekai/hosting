namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Creates instances of a service, resolving its dependencies from an
    /// <see cref="IHostServiceProvider"/> at creation time.
    /// </summary>
    /// <remarks>
    /// Factories are an AOT-safe alternative to reflection-based activation: the container
    /// invokes <see cref="Create"/> directly, so the concrete type and its constructor are known
    /// at compile time and no reflection metadata is required.
    /// </remarks>
    /// <typeparam name="TService">The service type produced by this factory.</typeparam>
    public interface IServiceFactory<TService>
    {
        /// <summary>
        /// Creates a new instance of <typeparamref name="TService"/>.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IHostServiceProvider"/> used to resolve the service's dependencies.
        /// </param>
        /// <returns>A newly created instance of <typeparamref name="TService"/>.</returns>
        TService Create(IHostServiceProvider provider);
    }
}