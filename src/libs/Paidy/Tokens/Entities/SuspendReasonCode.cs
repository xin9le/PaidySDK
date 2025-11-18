namespace Paidy.Tokens.Entities;



/// <summary>
/// Represents the Paidy-defined suspend reason codes.
/// </summary>
public enum SuspendReasonCode : byte
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
    /// fraud.detected
    /// </summary>
    FraudDetected,

    /// <summary>
    /// general
    /// </summary>
    General,
}
