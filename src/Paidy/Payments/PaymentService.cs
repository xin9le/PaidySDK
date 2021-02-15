using System.Collections.Generic;
using System.Net;
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
        /// When you are ready to charge the consumer, you perform a capture request.
        /// The payment must have a status of AUTHORIZED.
        /// All authorized requests automatically expire.
        /// The expiration period is specified in your contract and after this period, they are marked as expired and cannot be captured.
        /// </summary>
        /// <param name="id">Paidy payment ID</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-3-capture-a-payment"></a>
        /// </remarks>
        public ValueTask<PaymentResponse> CaptureAsync(string id, IDictionary<string, object>? metadata = default, CancellationToken cancellationToken = default)
        {
            var request = new CaptureRequest { Metadata = metadata };
            return this.CaptureAsync(id, request, cancellationToken);
        }


        /// <summary>
        /// When you are ready to charge the consumer, you perform a capture request.
        /// The payment must have a status of AUTHORIZED.
        /// All authorized requests automatically expire.
        /// The expiration period is specified in your contract and after this period, they are marked as expired and cannot be captured.
        /// </summary>
        /// <param name="id">Paidy payment ID</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-3-capture-a-payment"></a>
        /// </remarks>
        public async ValueTask<PaymentResponse> CaptureAsync(string id, CaptureRequest request = default, CancellationToken cancellationToken = default)
        {
            var url = $"payments/{id}/captures";
            var resolver = StandardResolver.AllowPrivateExcludeNull;
            var response = await this.HttpClient.PostAsJsonAsync(url, request, resolver, cancellationToken).ConfigureAwait(false);
            return await ReadContentAsync(response).ConfigureAwait(false);
        }


        /// <summary>
        /// Refunds all or part of a Paidy payment. You can only refund a payment that has captured.
        /// </summary>
        /// <param name="id">Paidy payment ID</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-4-refund-a-payment"></a>
        /// </remarks>
        public async ValueTask<PaymentResponse> RefundAsync(string id, RefundRequest request, CancellationToken cancellationToken = default)
        {
            var url = $"payments/{id}/refunds";
            var resolver = StandardResolver.AllowPrivateExcludeNull;
            var response = await this.HttpClient.PostAsJsonAsync(url, request, resolver, cancellationToken).ConfigureAwait(false);
            return await ReadContentAsync(response).ConfigureAwait(false);
        }


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
            return await ReadContentAsync(response).ConfigureAwait(false);
        }


        #region Helpers
        /// <summary>
        /// Reads the response content of the payment.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static async ValueTask<PaymentResponse> ReadContentAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var resolver = StandardResolver.AllowPrivate;
                return await response.Content.ReadFromJsonAsync<PaymentResponse>(resolver).ConfigureAwait(false);
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
