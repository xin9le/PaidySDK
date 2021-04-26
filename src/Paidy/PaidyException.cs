using System;
using System.Net;
using System.Text.Json;



namespace Paidy
{
    /// <summary>
    /// Represents an exception occurred by the Paidy API.
    /// </summary>
    public sealed class PaidyException : Exception
    {
        #region Properties
        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }


        /// <summary>
        /// Gets the error payload.
        /// </summary>
        public string Payload { get; }


        /// <summary>
        /// Gets the error information.
        /// </summary>
        public ErrorResponse Error
        {
            get
            {
                if (this.error is null)
                {
                    this.error = JsonSerializer.Deserialize<ErrorResponse>(this.Payload);
                    if (this.error is null)
                        throw new NotSupportedException("Null response was detected.");
                }
                return this.error;
            }
        }
        private ErrorResponse? error;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="payload"></param>
        internal PaidyException(HttpStatusCode statusCode, string payload)
            : this("An exception was occured while communicating with the Paidy API.", statusCode, payload)
        { }


        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        /// <param name="payload"></param>
        internal PaidyException(string message, HttpStatusCode statusCode, string payload)
            : this(message, null, statusCode, payload)
        { }


        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="statusCode"></param>
        /// <param name="payload"></param>
        internal PaidyException(string message, Exception? innerException, HttpStatusCode statusCode, string payload)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
            this.Payload = payload;
        }
        #endregion
    }
}
