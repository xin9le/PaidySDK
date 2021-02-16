using System.Collections.Generic;
using System.Runtime.Serialization;



namespace Paidy.Payments.Entities
{
    /// <summary>
    /// Represents the Paidy payment update request object.
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-6-update-a-payment"></a>
    /// </remarks>
    public readonly struct UpdateRequest
    {
        /// <summary>
        /// Merchant-assigned order or cart ID.
        /// In the API v2, this field is not required, and does not need to be unique.
        /// </summary>
        [DataMember(Name = "order_ref")]
        public string? OrderRef { get; init; }


        /// <summary>
        /// Description for this payment.
        /// </summary>
        [DataMember(Name = "description")]
        public string? Description { get; init; }


        /// <summary>
        /// Optional field in the payment object that can be used to store additional merchant-defined data about a payment.
        /// The field is a key-value map, limited to 20 keys.
        /// When you update a field, the existing values will be overwritten.
        /// So, if you are adding key-value pairs to an existing list in the metadata field, remember to include the existing key-value pairs in the update request.
        /// </summary>
        [DataMember(Name = "metadata")]
        public IDictionary<string, object>? Metadata { get; init; }
    }
}
