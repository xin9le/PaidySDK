namespace Paidy.Payments.Entities
{
    /// <summary>
    /// Represents the status of payment.
    /// </summary>
    public enum PaymentStatus : byte
    {
        /// <summary>
        /// A payment that is undefined status.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A payment that was successfully authorized.
        /// </summary>
        /// <remarks>AUTHORIZED</remarks>
        Authorized,

        /// <summary>
        /// A payment that was successfully captured, closed, or canceled.
        /// </summary>
        /// <remarks>CLOSED</remarks>
        Closed,
    }
}
