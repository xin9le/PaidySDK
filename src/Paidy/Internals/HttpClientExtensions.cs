using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;



namespace Paidy.Internals
{
    /// <summary>
    /// Provides extension methods for <see cref="HttpClient"/>.
    /// </summary>
    internal static class HttpClientExtensions
    {
#if NETSTANDARD2_0 || NET461
        private static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");


        /// <summary>
        /// Sends a PATCH request with a cancellation token to a Uri represented as a string as an asynchronous operation.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string? requestUri, HttpContent content, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(PatchMethod, requestUri) { Content = content };
            return client.SendAsync(request, cancellationToken);
        }
#endif


        /// <summary>
        /// Send the specified instance as JSON via POST method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="data"></param>
        /// <param name="resolver"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T data, IJsonFormatterResolver? resolver = default, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(data, resolver);
            using (var content = new ByteArrayContent(json))
            {
                content.Headers.ContentType = new("application/json");
                return await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// Send the specified instance as JSON via PUT method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="data"></param>
        /// <param name="resolver"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T data, IJsonFormatterResolver? resolver = default, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(data, resolver);
            using (var content = new ByteArrayContent(json))
            {
                content.Headers.ContentType = new("application/json");
                return await client.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// Send the specified instance as JSON via PATCH method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="data"></param>
        /// <param name="resolver"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, string requestUri, T data, IJsonFormatterResolver? resolver = default, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(data, resolver);
            using (var content = new ByteArrayContent(json))
            {
                content.Headers.ContentType = new("application/json");
                return await client.PatchAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
