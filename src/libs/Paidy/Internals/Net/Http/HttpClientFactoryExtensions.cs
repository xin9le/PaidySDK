using System.Net.Http;

namespace Paidy.Internals.Net.Http;



/// <summary>
/// Provides extension methods for <see cref="IHttpClientFactory"/>.
/// </summary>
internal static class HttpClientFactoryExtensions
{
    extension(IHttpClientFactory @this)
    {
        /// <summary>
        /// Create <see cref="HttpClient"/> instance for Paidy API.
        /// </summary>
        /// <returns></returns>
        public HttpClient ForPaidy()
            => @this.CreateClient(HttpClientNames.Paidy);
    }
}
