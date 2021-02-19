using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Paidy.Internals;
using Utf8Json;



namespace Paidy.Tokens.Entities
{
    /// <summary>
    /// Represents the Paidy token response object.
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#3-1-token-object"></a>
    /// </remarks>
    public sealed class TokenResponse
    {
#pragma warning disable CS8618
        #region Properties
        /// <summary>
        /// Unique identifier for the token.
        /// All token IDs begin with tok_.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; private init; }


        /// <summary>
        /// Paidy-generated merchant ID, beginning with mer_.
        /// </summary>
        [DataMember(Name = "merchant_id")]
        public string MerchantId { get; private init; }


        /// <summary>
        /// Merchant-provided wallet ID.
        /// You can use this field to group tokens.
        /// For example, large-scale merchants who have multiple sub-merchants or act as a payment service provider for other merchants can use the wallet_id to identify these sub-merchants.
        /// The default value is set to "default".
        /// </summary>
        [DataMember(Name = "wallet_id")]
        public string? WalletId { get; private init; }


        /// <summary>
        /// Status of the token.
        /// Valid values are: ACTIVE, SUSPENDED, or DELETED.
        /// </summary>
        [DataMember(Name = "status")]
        [JsonFormatter(typeof(TokenStatusFormatter))]
        public TokenStatus Status { get; private init; }


        /// <summary>
        /// Origin object which contains the buyer data and the consumer's shipping address.
        /// </summary>
        [DataMember(Name = "origin")]
        public OriginInfo Origin { get; private init; }


        /// <summary>
        /// Description for the token.
        /// </summary>
        [DataMember(Name = "description")]
        public string? Description { get; private init; }


        /// <summary>
        /// Payment type.
        /// Set to "recurring" for subscription payments.
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; private init; }


        /// <summary>
        /// Merchant-defined data about the object.
        /// This field is a key-value map, limited to 20 keys.
        /// </summary>
        [DataMember(Name = "metadata")]
        public IDictionary<string, object>? Metadata { get; private init; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "webhook_url")]
        [Obsolete("This field is currently not used.")]
        public string? WebhookUrl { get; private init; }


        /// <summary>
        /// Paidy-generated consumer ID, beginning with con_.
        /// </summary>
        [DataMember(Name = "consumer_id")]
        public string ConsumerId { get; private init; }


        /// <summary>
        /// If the token is suspended, this object contains data related to the suspension.
        /// suspensions object.
        /// </summary>
        [DataMember(Name = "suspensions")]
        public IReadOnlyList<SuspensionInfo> Suspensions { get; private init; }


        /// <summary>
        /// Flag to indicate whether the request is a test request.
        /// </summary>
        [DataMember(Name = "test")]
        public bool Test { get; private init; }


        /// <summary>
        /// A number that increments with each request that is executed against his token.
        /// This allows you to know if you are looking at the most recent version of the token.
        /// </summary>
        [DataMember(Name = "version_nr")]
        public int VersionNumber { get; private init; }


        /// <summary>
        /// Date and time the token was created, in UTC, and displayed in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "created_at")]
        public DateTimeOffset CreatedAt { get; private init; }


        /// <summary>
        /// Date and time the token was last updated, in UTC, and displayed in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "updated_at")]
        public DateTimeOffset UpdatedAt { get; private init; }


        /// <summary>
        /// Date and time the token was first activated, in UTC, and displayed in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "activated_at")]
        public DateTimeOffset ActivatedAt { get; private init; }


        /// <summary>
        /// Date and time the token was deleted, in UTC, and displayed in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "deleted_at")]
        [JsonFormatter(typeof(IgnoreWhiteSpaceISO8601DateTimeOffsetFormatter))]
        public DateTimeOffset? DeletedAt { get; private init; }
        #endregion



        #region Internal Types
        /// <summary>
        /// Represents consumer object.
        /// </summary>
        public sealed class OriginInfo
        {
            /// <summary>
            /// Consumer's name in kanji.
            /// Family name and first name must be separated by a space, e.g., 山田　太郎.
            /// </summary>
            [DataMember(Name = "name1")]
            public string Name1 { get; private init; }


            /// <summary>
            /// Consumer's name in katakana.
            /// Family name and first name must be separated by a space, e.g., ヤマダ　タロウ.
            /// </summary>
            [DataMember(Name = "name2")]
            public string? Name2 { get; private init; }


            /// <summary>
            /// Consumer's email address.
            /// </summary>
            [DataMember(Name = "email")]
            public string? Email { get; private init; }


            /// <summary>
            /// Consumer's phone number, e.g., 09011112222.
            /// This must be a Japanese mobile phone where the consumer can receive text messages.
            /// </summary>
            [DataMember(Name = "phone")]
            public string? Phone { get; private init; }


            /// <summary>
            /// Consumer's address.
            /// </summary>
            [DataMember(Name = "address")]
            public AddressInfo Address { get; private init; }
        }


        /// <summary>
        /// Represents address object.
        /// </summary>
        public sealed class AddressInfo
        {
            /// <summary>
            /// Building name, apartment number.
            /// </summary>
            [DataMember(Name = "line1")]
            public string? Line1 { get; private init; }


            /// <summary>
            /// District, land number, land extension number.
            /// </summary>
            [DataMember(Name = "line2")]
            public string? Line2 { get; private init; }


            /// <summary>
            /// Name of city, municipality, or village.
            /// </summary>
            [DataMember(Name = "city")]
            public string? City { get; private init; }


            /// <summary>
            /// Prefecture.
            /// </summary>
            [DataMember(Name = "state")]
            public string? State { get; private init; }


            /// <summary>
            /// Postal code; format is NNN-NNNN.
            /// </summary>
            [DataMember(Name = "zip")]
            public string Zip { get; private init; }
        }


        /// <summary>
        /// Represents suspension object.
        /// </summary>
        public sealed class SuspensionInfo
        {
            /// <summary>
            /// Date and time the token was suspended, in UTC, and displayed in ISO 8601 format.
            /// </summary>
            [DataMember(Name = "timestamp")]
            public DateTimeOffset Timestamp { get; private init; }


            /// <summary>
            /// The person responsible for suspending the token; for API requests, this is set to "merchant".
            /// </summary>
            [DataMember(Name = "authority")]
            public string Authority { get; private init; }
        }
        #endregion
#pragma warning restore CS8618
    }
}
