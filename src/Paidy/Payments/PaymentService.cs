﻿using System.Net;
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
        /// Creates a new subscription payment using a token.
        /// The token and the consumer must have a status of ACTIVE.
        /// </summary>
        /// <param name="id">Paidy payment ID</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-2-create-a-payment"></a>
        /// </remarks>
        public async ValueTask<PaymentResponse> CreateAsync(string id, CreateRequest request, CancellationToken cancellationToken = default)
        {
            const string url = "payments";
            var resolver = StandardResolver.ExcludeNull;
            var response = await this.HttpClient.PostAsJsonAsync(url, request, resolver, cancellationToken).ConfigureAwait(false);
            return await ReadContentAsync(response).ConfigureAwait(false);
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
            var resolver = StandardResolver.ExcludeNull;
            var response = await this.HttpClient.PostAsJsonAsync(url, request, resolver, cancellationToken).ConfigureAwait(false);
            return await ReadContentAsync(response).ConfigureAwait(false);
        }


        /// <summary>
        /// Refunds all of a Paidy payment.
        /// You can only refund a payment that has captured.
        /// </summary>
        /// <param name="id">Paidy payment ID</param>
        /// <param name="captureId">Paidy capture ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-4-refund-a-payment"></a>
        /// </remarks>
        public ValueTask<PaymentResponse> RefundAsync(string id, string captureId, CancellationToken cancellationToken = default)
        {
            var request = new RefundRequest { CaptureId = captureId };
            return this.RefundAsync(id, request, cancellationToken);
        }


        /// <summary>
        /// Refunds all or part of a Paidy payment.
        /// You can only refund a payment that has captured.
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
            var resolver = StandardResolver.ExcludeNull;
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


        /// <summary>
        /// Updates the order_ref, description, and/or metadata fields for a payment.
        /// You can only use this endpoint to update these 3 fields.
        /// If you send other fields in the request, they will simply be ignored by Paidy.
        /// The payment to be updated can have a status of AUTHORIZED or CLOSED.
        /// </summary>
        /// <param name="id">Paidy payment ID</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-6-update-a-payment"></a>
        /// </remarks>
        public async ValueTask<PaymentResponse> UpdateAsync(string id, UpdateRequest request = default, CancellationToken cancellationToken = default)
        {
            var url = $"payments/{id}";
            var resolver = StandardResolver.ExcludeNull;
            var response = await this.HttpClient.PutAsJsonAsync(url, request, resolver, cancellationToken).ConfigureAwait(false);
            return await ReadContentAsync(response).ConfigureAwait(false);
        }


        /// <summary>
        /// Closes a Paidy payment that was successfully authorized, but not captured.
        /// The payment must have a status of AUTHORIZED.
        /// </summary>
        /// <param name="id">Paidy payment ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-7-close-a-payment"></a>
        /// </remarks>
        public async ValueTask<PaymentResponse> CloseAsync(string id, CancellationToken cancellationToken = default)
        {
            var url = $"payments/{id}/close";
            var response = await this.HttpClient.PostAsync(url, null!, cancellationToken).ConfigureAwait(false);
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
