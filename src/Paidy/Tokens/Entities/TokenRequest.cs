using System.Runtime.Serialization;



namespace Paidy.Tokens.Entities
{
    /// <summary>
    /// Represents the Paidy token request object.
    /// </summary>
    /// <remarks>
    /// Reference :<br/>
    /// - <a href="https://paidy.com/docs/api/en/index.html#3-2-suspend-a-token"></a><br/>
    /// - <a href="https://paidy.com/docs/api/en/index.html#3-3-resume-a-token"></a><br/>
    /// - <a href="https://paidy.com/docs/api/en/index.html#3-4-delete-a-token"></a><br/>
    /// </remarks>
    public sealed class TokenRequest
    {
#pragma warning disable CS8618
        /// <summary>
        /// Merchant-provided wallet ID. Default value is "default".
        /// When the token was created, if the value of this field was set to "default", this field is optional.
        /// However, if a merchant-specific wallet ID was set, this field is required.
        /// If you do not sent this field or you send the incorrect value, Paidy will return an error.
        /// </summary>
        [DataMember(Name = "wallet_id")]
        public string? WalletId { get; init; }


        /// <summary>
        /// Reason for the request.
        /// </summary>
        [DataMember(Name = "reason")]
        public ReasonInfo Reason { get; init; }



        #region Internal Types
        /// <summary>
        /// Represents reason data object.
        /// </summary>
        public sealed class ReasonInfo
        {
            /// <summary>
            /// Paidy-defined reason code for the request.
            /// </summary>
            [DataMember(Name = "code")]
            public string Code { get; init; }


            /// <summary>
            /// Additional, more specific details about the reason.
            /// This field cannot be empty.
            /// </summary>
            [DataMember(Name = "description")]
            public string Description { get; init; }
        }
        #endregion
#pragma warning restore CS8618
    }
}
