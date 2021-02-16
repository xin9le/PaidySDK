using System;
using System.IO;
using System.Runtime.Serialization;
using Paidy.Internals;
using Utf8Json;
using Utf8Json.Resolvers;



namespace Paidy.Webhooks.Entities
{
    /// <summary>
    /// Represents the Paidy payment webhook request object.
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/en/webhook.html"></a>
    /// </remarks>
    public sealed class PaymentRequest
    {
        #region Properties
        /// <summary>
        /// Paidy payment ID.
        /// </summary>
        [DataMember(Name = "payment_id")]
        public string PaymentId { get; private init; }


        /// <summary>
        /// Paidy capture ID.
        /// Only sent for capture and refund events.
        /// </summary>
        [DataMember(Name = "capture_id")]
        public string? CaptureId { get; private init; }


        /// <summary>
        /// Paidy only sends webhooks for payment events.
        /// Set to "payment".
        /// </summary>
        [DataMember(Name = "event_type")]
        public string EventType { get; private init; }


        /// <summary>
        /// Indicates both the type of payment event and the result of the request.
        /// </summary>
        [DataMember(Name = "status")]
        [JsonFormatter(typeof(PaymentEventFormatter))]
        public PaymentEvent Status { get; private init; }


        /// <summary>
        /// Merchant’s own unique reference for the order.
        /// </summary>
        [DataMember(Name = "order_ref")]
        public string? OrderRef { get; private init; }


        /// <summary>
        /// Reason for the refund.
        /// Only sent for refund events.
        /// </summary>
        [DataMember(Name = "reason")]
        public string? Reason { get; private init; }


        /// <summary>
        /// Date and time in UTC that the event was created, displayed in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "timestamp")]
        public DateTimeOffset Timestamp { get; private init; }
        #endregion


        #region Constructors
#pragma warning disable CS8618
        /// <summary>
        /// Creates an instance.
        /// </summary>
        private PaymentRequest()
        { }
#pragma warning restore CS8618
        #endregion


        #region Methods
        /// <summary>
        /// Creates an instance from a <see cref="byte"/>[] containing JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static PaymentRequest From(byte[] json)
            => JsonSerializer.Deserialize<PaymentRequest>(json, StandardResolver.AllowPrivate);


        /// <summary>
        /// Creates an instance from a <see cref="string"/> containing JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static PaymentRequest From(string json)
            => JsonSerializer.Deserialize<PaymentRequest>(json, StandardResolver.AllowPrivate);


        /// <summary>
        /// Creates an instance from a <see cref="Stream"/> containing JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static PaymentRequest From(Stream json)
            => JsonSerializer.Deserialize<PaymentRequest>(json, StandardResolver.AllowPrivate);
        #endregion
    }
}
