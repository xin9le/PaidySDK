using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;



namespace Paidy.Internals
{
    /// <summary>
    /// Provides extension methods for <see cref="HttpClient"/>.
    /// </summary>
    internal static class HttpClientExtensions
    {
#if NETSTANDARD2_0 || NET461_OR_GREATER
        private static readonly HttpMethod PatchMethod = new("PATCH");


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
        /// Send a PATCH request to the specified Uri containing the value serialized as JSON in the request body.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="client">The client used to send the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="options">Options to control the behavior during serialization, the default options are System.Text.Json.JsonSerializerDefaults.Web.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, string? requestUri, T value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            options ??= JsonSerializerOptionsProvider.Web;
            var json = JsonSerializer.SerializeToUtf8Bytes(value, options);
            using (var content = new ByteArrayContent(json))
            {
                content.Headers.ContentType = new("application/json");
                return await client.PatchAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// Send a PATCH request to the specified Uri containing the value serialized as JSON in the request body.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="client">The client used to send the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken)
            => client.PatchAsJsonAsync(requestUri, value, options: null, cancellationToken);
    }
}
