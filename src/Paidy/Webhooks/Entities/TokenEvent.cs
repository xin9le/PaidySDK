namespace Paidy.Webhooks.Entities
{
    /// <summary>
    /// Represents the status of payment.
    /// </summary>
    public enum TokenEvent : byte
    {
        /// <summary>
        /// A payment that is undefined status.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A payment that was successfully activated.
        /// </summary>
        /// <remarks>activate_success</remarks>
        ActivateSuccess,

        /// <summary>
        /// A payment that was successfully suspended.
        /// </summary>
        /// <remarks>suspend_success</remarks>
        SuspendSuccess,

        /// <summary>
        /// A payment that was successfully resumeed.
        /// </summary>
        /// <remarks>resume_success</remarks>
        ResumeSuccess,

        /// <summary>
        /// A payment that was successfully deleted.
        /// </summary>
        /// <remarks>delete_success</remarks>
        DeleteSuccess,
    }
}
