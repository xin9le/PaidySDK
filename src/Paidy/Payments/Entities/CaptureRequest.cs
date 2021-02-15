using System.Collections.Generic;
using System.Runtime.Serialization;



namespace Paidy.Payments.Entities
{
    /// <summary>
    /// Represents the Paidy payment capture request object.
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-3-capture-a-payment"></a>
    /// </remarks>
    public readonly struct CaptureRequest
    {
        /// <summary>
        /// You can use this field to store additional structured information about the capture.
        /// It is a key-value map, limited to 20 keys.
        /// </summary>
        [DataMember(Name = "metadata")]
        public IDictionary<string, object>? Metadata { get; init; }
    }
}
