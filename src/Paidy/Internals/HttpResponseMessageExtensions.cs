using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Paidy.Payments;
using Paidy.Payments.Entities;
using Utf8Json.Resolvers;



namespace Paidy.Internals
{
    /// <summary>
    /// Provides extension methods for <see cref="HttpResponseMessage"/>.
    /// </summary>
    internal static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Reads the response content of the payment.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async ValueTask<PaymentResponse> ReadPaymentContentAsync(this HttpResponseMessage response)
        {
            var resolver = StandardResolver.AllowPrivate;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<PaymentResponse>(resolver).ConfigureAwait(false);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new PaymentException(response.StatusCode, error);
            }
        }
    }
}
