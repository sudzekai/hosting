namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Provides actions to register services and build an <see cref="IHostServiceProvider"/>.
    /// </summary>
    /// <remarks>
    /// A <c>transient</c> service (also known as scoped) is resolved anew on every
    /// <see cref="IHostServiceProvider.GetRequiredService{TService}"/> call, so each request
    /// gets a fresh instance.
    /// </remarks>
    public partial interface IHostBuilderServiceCollection
    {
        // ===================== LinQ Generated

        /// <summary>
        /// Registers a transient service whose instances are created via <c>new()</c>.
        /// </summary>
        /// <remarks>
        /// A new instance is returned on every resolution.
        /// </remarks>
        /// <typeparam name="TService">The service type.</typeparam>
        IHostBuilderServiceCollection AddTransiet<TService>()
            where TService : class, new();

        /// <summary>
        /// Registers a transient service under <typeparamref name="TBase"/> whose instances are
        /// created via <c>new()</c>.
        /// </summary>
        /// <remarks>
        /// A new instance is returned on every resolution.
        /// </remarks>
        /// <typeparam name="TBase">The base type used as the registration key.</typeparam>
        /// <typeparam name="TService">The service implementation type.</typeparam>
        IHostBuilderServiceCollection AddTransiet<TBase, TService>()
            where TBase : class
            where TService : class, TBase, new();

        // ===================== Factory Generated

        /// <summary>
        /// Registers a transient service created by the provided <see cref="IServiceFactory{TService}"/>.
        /// </summary>
        /// <remarks>
        /// A new instance is returned on every resolution.
        /// </remarks>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="factory">The factory used to create service instances.</param>
        IHostBuilderServiceCollection AddTransietFactory<TService>(IServiceFactory<TService> factory)
            where TService : class;

        // ===================== Factory Function Generated

        /// <summary>
        /// Registers a transient service created by the provided factory method.
        /// </summary>
        /// <remarks>
        /// A new instance is returned on every resolution.
        /// </remarks>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="factoryMethod">The delegate used to create service instances.</param>
        IHostBuilderServiceCollection AddTransietFactory<TService>(Func<IHostServiceProvider, TService> factoryMethod)
            where TService : class;
    }
}