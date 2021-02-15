using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;



namespace Paidy.Internals
{
    /// <summary>
    /// Provides extension methods for <see cref="HttpContent"/>.
    /// </summary>
    internal static class HttpContentExtensions
    {
        /// <summary>
        /// Parses as JSON and deserializes to the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="resolver"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask<T> ReadFromJsonAsync<T>(this HttpContent content, IJsonFormatterResolver? resolver = default, CancellationToken cancellationToken = default)
        {
#if NETSTANDARD
            var payload = await content.ReadAsByteArrayAsync().ConfigureAwait(false);
#else
            var payload = await content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
#endif
            return JsonSerializer.Deserialize<T>(payload, resolver);
        }
    }
}
