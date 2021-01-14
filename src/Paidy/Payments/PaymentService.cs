using System.Net.Http;



namespace Paidy.Payments
{
    /// <summary>
    /// Provides the payment API service.
    /// </summary>
    public sealed class PaymentService
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
        internal PaymentService(HttpClient client)
            => this.HttpClient = client;
        #endregion
    }
}
