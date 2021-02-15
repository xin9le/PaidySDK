using System;
using System.Net;
using Paidy.Payments.Entities;
using Utf8Json;
using Utf8Json.Resolvers;



namespace Paidy.Payments
{
    /// <summary>
    /// Represents an exception occurred by the Paidy API.
    /// </summary>
    public sealed class PaymentException : Exception
    {
        #region Properties
        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }


        /// <summary>
        /// Gets the raw error information.
        /// </summary>
        public string RawError { get; }


        /// <summary>
        /// Gets the error information.
        /// </summary>
        public ErrorResponse ErrorResponse
            => this.errorResponse
            ??= JsonSerializer.Deserialize<ErrorResponse>(this.RawError, StandardResolver.AllowPrivate);
        private ErrorResponse? errorResponse;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="rawError"></param>
        internal PaymentException(HttpStatusCode statusCode, string rawError)
            : this("An exception was occured while communicating with the Paidy API.", statusCode, rawError)
        { }


        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        /// <param name="rawError"></param>
        internal PaymentException(string message, HttpStatusCode statusCode, string rawError)
            : this(message, null, statusCode, rawError)
        { }


        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="statusCode"></param>
        /// <param name="rawError"></param>
        internal PaymentException(string message, Exception? innerException, HttpStatusCode statusCode, string rawError)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
            this.RawError = rawError;
        }
        #endregion
    }
}
