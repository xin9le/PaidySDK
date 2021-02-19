namespace Paidy.Tokens.Entities
{
    /// <summary>
    /// Represents the status of token.
    /// </summary>
    public enum TokenStatus : byte
    {
        /// <summary>
        /// A token that is undefined status.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A token that was successfully authorized.
        /// </summary>
        /// <remarks>ACTIVE</remarks>
        Active,

        /// <summary>
        /// A token that was successfully suspended.
        /// </summary>
        /// <remarks>SUSPENDED</remarks>
        Suspended,

        /// <summary>
        /// A token that was successfully deleted.
        /// </summary>
        /// <remarks>DELETED</remarks>
        Deleted,
    }
}
