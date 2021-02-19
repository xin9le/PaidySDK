using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Paidy.Internals;
using Paidy.Tokens.Entities;
using Utf8Json.Resolvers;



namespace Paidy.Tokens
{
    /// <summary>
    /// Provides the token API service.
    /// </summary>
    public sealed class TokenService
    {
        #region Properties
        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        private HttpClient HttpClient { get; }
        #endregion


        #region Constructors
        /// <summary>
        /// Creates instance.
        /// </summary>
        internal TokenService(HttpClient client)
            => this.HttpClient = client;
        #endregion


        /// <summary>
        /// Retrieves the specified token object.
        /// You need a valid token ID, beginning with tok_.
        /// </summary>
        /// <param name="id">Paidy token ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#3-5-retrieve-a-token"></a>
        /// </remarks>
        public async ValueTask<TokenResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            var url = $"tokens/{id}";
            var response = await this.HttpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            return await ReadContentAsync(response).ConfigureAwait(false);
        }


        #region Helpers
        /// <summary>
        /// Reads the response content of the token.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static async ValueTask<TokenResponse> ReadContentAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var resolver = StandardResolver.AllowPrivate;
                return await response.Content.ReadFromJsonAsync<TokenResponse>(resolver).ConfigureAwait(false);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new PaidyException(response.StatusCode, error);
            }
        }
        #endregion
    }
}
