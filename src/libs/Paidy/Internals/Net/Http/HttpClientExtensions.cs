using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Paidy.Internals.Text.Json;

namespace Paidy.Internals.Net.Http;



/// <summary>
/// Provides extension methods for <see cref="HttpClient"/>.
/// </summary>
internal static class HttpClientExtensions
{
#if NETSTANDARD2_0 || NET462_OR_GREATER
    private static readonly HttpMethod PatchMethod = new("PATCH");


    extension(HttpClient @this)
    {
        /// <summary>
        /// Sends a PATCH request with a cancellation token to a Uri represented as a string as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<HttpResponseMessage> PatchAsync(string? requestUri, HttpContent content, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(PatchMethod, requestUri) { Content = content };
            var response = await @this.SendAsync(request, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
#endif


    extension(HttpClient @this)
    {
        /// <summary>
        /// Send a PATCH request to the specified Uri containing the value serialized as JSON in the request body.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="options">Options to control the behavior during serialization, the default options are System.Text.Json.JsonSerializerDefaults.Web.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<HttpResponseMessage> PatchAsJsonAsync<T>(string? requestUri, T value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            options ??= JsonSerializerOptionsProvider.Web;
            var json = JsonSerializer.SerializeToUtf8Bytes(value, options);
            using (var content = new ByteArrayContent(json))
            {
                content.Headers.ContentType = new("application/json");
                return await @this.PatchAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// Send a PATCH request to the specified Uri containing the value serialized as JSON in the request body.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<HttpResponseMessage> PatchAsJsonAsync<T>(string? requestUri, T value, CancellationToken cancellationToken)
        {
            var response = await @this.PatchAsJsonAsync(requestUri, value, options: null, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
