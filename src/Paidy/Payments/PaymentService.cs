using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Paidy.Internals;
using Paidy.Payments.Entities;
using Utf8Json.Resolvers;



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


        /// <summary>
        /// Retrieves the specified payment.
        /// If successful, returns the entire payment object, including the status, any captures, and any refunds.
        /// </summary>
        /// <param name="id">Paidy payment ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-5-retrieve-a-payment"></a>
        /// </remarks>
        public async ValueTask<PaymentResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            var url = $"payments/{id}";
            var response = await this.HttpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var resolver = StandardResolver.AllowPrivate;
            return await response.Content.ReadFromJsonAsync<PaymentResponse>(resolver).ConfigureAwait(false);
        }
    }
}
