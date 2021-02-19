namespace Paidy.Tokens.Entities
{
    /// <summary>
    /// Represents the Paidy-defined resume reason codes.
    /// </summary>
    public enum ResumeReasonCode : byte
    {
        /// <summary>
        /// consumer.requested
        /// </summary>
        ConsumerRequested = 0,

        /// <summary>
        /// merchant.requested
        /// </summary>
        MerchantRequested,

        /// <summary>
        /// general
        /// </summary>
        General,
    }
}
