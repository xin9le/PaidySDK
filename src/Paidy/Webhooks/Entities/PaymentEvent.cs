namespace Paidy.Webhooks.Entities;



/// <summary>
/// Represents the status of payment.
/// </summary>
public enum PaymentEvent : byte
{
    /// <summary>
    /// A payment that is undefined status.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// A payment that was successfully authorized.
    /// </summary>
    /// <remarks>authorize_success</remarks>
    AuthorizeSuccess,

    /// <summary>
    /// A payment that was successfully captured.
    /// </summary>
    /// <remarks>capture_success</remarks>
    CaptureSuccess,

    /// <summary>
    /// A payment that was successfully refunded.
    /// </summary>
    /// <remarks>refund_success</remarks>
    RefundSuccess,

    /// <summary>
    /// A payment that was successfully updated.
    /// </summary>
    /// <remarks>update_success</remarks>
    UpdateSuccess,

    /// <summary>
    /// A payment that was successfully closed.
    /// </summary>
    /// <remarks>close _success</remarks>
    CloseSuccess,
}
