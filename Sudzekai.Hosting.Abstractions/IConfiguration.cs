namespace Sudzekai.Hosting.Abstractions
{
    /// <summary>
    /// Provides access to the configuration values loaded for the host.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the value stored under <paramref name="key"/>.
        /// </summary>
        /// <remarks>
        /// Nested sections are flattened with the <c>:</c> separator (for example
        /// <c>"Database:ConnectionString"</c>), and array items are indexed (for example
        /// <c>"Items[0]"</c>). Returns <see langword="null"/> when the key is not present.
        /// </remarks>
        /// <param name="key">The configuration key.</param>
        /// <returns>
        /// The value whose key matches <paramref name="key"/>, or <see langword="null"/>
        /// if no such key exists.
        /// </returns>
        object? this[string key] { get; }
    }
}