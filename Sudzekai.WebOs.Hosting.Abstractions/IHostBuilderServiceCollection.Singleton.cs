namespace Sudzekai.Hosting.Abstractions
{
    public partial interface IHostBuilderServiceCollection
    {
        // ===================== Predefined

        /// <summary>
        /// Registers a singleton service using a pre-created instance.
        /// </summary>
        /// <remarks>
        /// The registered instance is reused for the whole host lifetime:
        /// <see cref="IHostServiceProvider.GetRequiredService{TService}"/> returns this exact instance.
        /// </remarks>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="instance">The service instance to register.</param>
        IHostBuilderServiceCollection AddSingleton<TService>(TService instance)
            where TService : class;

        /// <summary>
        /// Registers a singleton service under <typeparamref name="TBase"/> using a pre-created instance.
        /// </summary>
        /// <remarks>
        /// The registered instance is reused for the whole host lifetime:
        /// <see cref="IHostServiceProvider.GetRequiredService{TService}"/> returns this exact instance.
        /// </remarks>
        /// <typeparam name="TBase">The base type used as the registration key.</typeparam>
        /// <typeparam name="TService">The service implementation type.</typeparam>
        /// <param name="instance">The service instance to register.</param>
        IHostBuilderServiceCollection AddSingleton<TBase, TService>(TService instance)
            where TBase : class
            where TService : class, TBase;

        // ===================== LinQ Generated

        /// <summary>
        /// Registers a singleton service whose instance is created lazily via <c>new()</c>.
        /// </summary>
        /// <remarks>
        /// The instance is created on first use and reused for the whole host lifetime.
        /// </remarks>
        /// <typeparam name="TService">The service type.</typeparam>
        IHostBuilderServiceCollection AddSingleton<TService>()
            where TService : class, new();

        /// <summary>
        /// Registers a singleton service under <typeparamref name="TBase"/> whose instance is
        /// created lazily via <c>new()</c>.
        /// </summary>
        /// <remarks>
        /// The instance is created on first use and reused for the whole host lifetime.
        /// </remarks>
        /// <typeparam name="TBase">The base type used as the registration key.</typeparam>
        /// <typeparam name="TService">The service implementation type.</typeparam>
        IHostBuilderServiceCollection AddSingleton<TBase, TService>()
            where TBase : class
            where TService : class, TBase, new();

        // ===================== Factory Generated

        /// <summary>
        /// Registers a singleton service created by the provided <see cref="IServiceFactory{TService}"/>.
        /// </summary>
        /// <remarks>
        /// The instance is created on first use and reused for the whole host lifetime.
        /// </remarks>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="factory">The factory used to create the service instance.</param>
        IHostBuilderServiceCollection AddSingletonFactory<TService>(IServiceFactory<TService> factory)
            where TService : class;

        // ===================== Factory Function Generated

        /// <summary>
        /// Registers a singleton service created by the provided factory method.
        /// </summary>
        /// <remarks>
        /// The instance is created on first use and reused for the whole host lifetime.
        /// </remarks>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="factoryMethod">The delegate used to create the service instance.</param>
        IHostBuilderServiceCollection AddSingletonFactory<TService>(Func<IHostServiceProvider, TService> factoryMethod)
            where TService : class;

    }
}