using System.Diagnostics.CodeAnalysis;

namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Provides access to the services registered in the host's service collection.
    /// </summary>
    public interface IHostServiceProvider
    {
        /// <summary>
        /// Attempts to resolve a service registered under <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The base or implementation type of the service to resolve.</typeparam>
        /// <param name="service">
        /// When this method returns <see langword="true"/>, contains the resolved service;
        /// otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the service was resolved; otherwise, <see langword="false"/>.
        /// </returns>
        bool TryGetRequiredService<T>([NotNullWhen(true)] out T? service);

        /// <summary>
        /// Resolves a service registered under <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The base or implementation type of the service to resolve.</typeparam>
        /// <returns>The resolved service instance.</returns>
        /// <remarks>
        /// Throws an exception when no service is registered under <typeparamref name="T"/>.
        /// Use <see cref="TryGetRequiredService{T}"/> to resolve without throwing.
        /// </remarks>
        T GetRequiredService<T>();
    }
}