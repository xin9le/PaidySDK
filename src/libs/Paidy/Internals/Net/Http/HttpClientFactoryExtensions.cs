using System.Net.Http;

namespace Paidy.Internals.Net.Http;



/// <summary>
/// Provides extension methods for <see cref="IHttpClientFactory"/>.
/// </summary>
internal static class HttpClientFactoryExtensions
{
    /// <summary>
    /// Create <see cref="HttpClient"/> instance for Paidy API.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static HttpClient ForPaidy(this IHttpClientFactory factory)
        => factory.CreateClient(HttpClientNames.Paidy);
}
