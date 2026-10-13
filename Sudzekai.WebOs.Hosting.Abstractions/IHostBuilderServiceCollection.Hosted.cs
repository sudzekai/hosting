namespace Sudzekai.Hosting.Abstractions
{
    public partial interface IHostBuilderServiceCollection
    {
        // ===================== Predefined

        /// <summary>
        /// Registers a hosted service using a pre-created instance.
        /// </summary>
        /// <remarks>
        /// The registered instance is reused for the whole host lifetime:
        /// <see cref="IHostServiceProvider.GetRequiredService{TService}"/> returns this exact instance.
        /// </remarks>
        /// <typeparam name="TService">The hosted service type.</typeparam>
        /// <param name="instance">The hosted service instance to register.</param>
        IHostBuilderServiceCollection AddHosted<TService>(TService instance)
            where TService : class, IHostedService;

        /// <summary>
        /// Registers a hosted service under <typeparamref name="TBase"/> using a pre-created instance.
        /// </summary>
        /// <remarks>
        /// The registered instance is reused for the whole host lifetime:
        /// <see cref="IHostServiceProvider.GetRequiredService{TService}"/> returns this exact instance.
        /// </remarks>
        /// <typeparam name="TBase">The base type used as the registration key.</typeparam>
        /// <typeparam name="TService">The hosted service implementation type.</typeparam>
        /// <param name="instance">The hosted service instance to register.</param>
        IHostBuilderServiceCollection AddHosted<TBase, TService>(TService instance)
            where TBase : class, IHostedService
            where TService : class, TBase;

        // ===================== LinQ Generated

        /// <summary>
        /// Registers a hosted service whose instance is created lazily via <c>new()</c>.
        /// </summary>
        /// <remarks>
        /// The instance is created on first use and reused for the whole host lifetime:
        /// <see cref="IHostServiceProvider.GetRequiredService{TService}"/> returns that instance.
        /// </remarks>
        /// <typeparam name="TService">The hosted service type.</typeparam>
        IHostBuilderServiceCollection AddHosted<TService>()
            where TService : class, IHostedService, new();

        /// <summary>
        /// Registers a hosted service under <typeparamref name="TBase"/> whose instance is
        /// created lazily via <c>new()</c>.
        /// </summary>
        /// <remarks>
        /// The instance is created on first use and reused for the whole host lifetime.
        /// </remarks>
        /// <typeparam name="TBase">The base type used as the registration key.</typeparam>
        /// <typeparam name="TService">The hosted service implementation type.</typeparam>
        IHostBuilderServiceCollection AddHosted<TBase, TService>()
            where TBase : class, IHostedService
            where TService : class, TBase, new();

        // ===================== Factory Generated

        /// <summary>
        /// Registers a hosted service created by the provided <see cref="IServiceFactory{TService}"/>.
        /// </summary>
        /// <remarks>
        /// The instance is created on first use and reused for the whole host lifetime.
        /// </remarks>
        /// <typeparam name="TService">The hosted service type.</typeparam>
        /// <param name="factory">The factory used to create the hosted service instance.</param>
        IHostBuilderServiceCollection AddHostedFactory<TService>(IServiceFactory<TService> factory)
            where TService : class, IHostedService;

        // ===================== Factory Function Generated

        /// <summary>
        /// Registers a hosted service created by the provided factory method.
        /// </summary>
        /// <remarks>
        /// The instance is created on first use and reused for the whole host lifetime.
        /// </remarks>
        /// <typeparam name="TService">The hosted service type.</typeparam>
        /// <param name="factoryMethod">The delegate used to create the hosted service instance.</param>
        IHostBuilderServiceCollection AddHostedFactory<TService>(Func<IHostServiceProvider, TService> factoryMethod)
            where TService : class, IHostedService;
    }
}