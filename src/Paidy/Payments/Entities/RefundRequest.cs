using System.Collections.Generic;
using System.Runtime.Serialization;



namespace Paidy.Payments.Entities
{
    /// <summary>
    /// Represents the Paidy payment refund request object.
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-3-capture-a-payment"></a>
    /// </remarks>
    public sealed class RefundRequest
    {
#pragma warning disable CS8618
        /// <summary>
        /// Since a refund request is executed on the specified capture, the capture_id must be included in the request.
        /// All capture IDs begin with cap_.
        /// </summary>
        [DataMember(Name = "capture_id")]
        public string CaptureId { get; init; }
#pragma warning restore CS8618


        /// <summary>
        /// The amount to refund.
        /// Paidy uses this field to determine whether the request is for a partial refund or full refund.
        /// If no amount is specified, Paidy refunds the full amount for that capture.
        /// </summary>
        [DataMember(Name = "amount")]
        public decimal? Amount { get; init; }


        /// <summary>
        /// The reason for the refund.
        /// </summary>
        [DataMember(Name = "reason")]
        public string? Reason { get; init; }


        /// <summary>
        /// You can use this field to store additional structured information about the refund.
        /// It is a key-value map, limited to 20 keys.
        /// </summary>
        [DataMember(Name = "metadata")]
        public IDictionary<string, object>? Metadata { get; init; }
    }
}
