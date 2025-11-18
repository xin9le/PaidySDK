using System.Net.Http;

namespace Paidy.Internals;



/// <summary>
/// Provides the registered name of the <see cref="HttpClient"/> to be used by <see cref="IHttpClientFactory"/>.
/// </summary>
internal static class HttpClientNames
{
    /// <summary>
    /// Shared name.
    /// </summary>
    public const string Paidy = "___Paidy.HttpClient___";
}
