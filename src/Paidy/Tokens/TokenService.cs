using System.Net.Http;



namespace Paidy.Tokens
{
    /// <summary>
    /// Provides the payment API service.
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
    }
}
