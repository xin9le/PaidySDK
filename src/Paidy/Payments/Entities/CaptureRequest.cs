using System.Collections.Generic;
using System.Runtime.Serialization;



namespace Paidy.Payments.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-3-capture-a-payment"></a>
    /// </remarks>
    public readonly struct CaptureRequest
    {
        /// <summary>
        /// Merchant-defined data about the capture object.
        /// This field is a key-value map, limited to 20 keys.
        /// </summary>
        [DataMember(Name = "metadata")]
        public IDictionary<string, object>? Metadata { get; init; }
    }
}
