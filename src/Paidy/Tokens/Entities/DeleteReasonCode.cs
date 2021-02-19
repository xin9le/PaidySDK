namespace Paidy.Tokens.Entities
{
    /// <summary>
    /// Represents the Paidy-defined delete reason codes.
    /// </summary>
    public enum DeleteReasonCode : byte
    {
        /// <summary>
        /// consumer.requested
        /// </summary>
        ConsumerRequested = 0,

        /// <summary>
        /// subscription.expired
        /// </summary>
        SubscriptionExpired,

        /// <summary>
        /// merchant.requested
        /// </summary>
        MerchantRequested,

        /// <summary>
        /// fraud.detected
        /// </summary>
        FraudDetected,

        /// <summary>
        /// general
        /// </summary>
        General,
    }
}
