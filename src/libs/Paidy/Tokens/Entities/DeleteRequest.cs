using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Paidy.Internals.Text.Json;

namespace Paidy.Tokens.Entities;



/// <summary>
/// Represents the Paidy token delete request object.
/// </summary>
/// <remarks>
/// Reference : <a href="https://paidy.com/docs/api/en/index.html#3-4-delete-a-token"></a>
/// </remarks>
public sealed class DeleteRequest
{
#pragma warning disable CS8618
    /// <summary>
    /// Merchant-provided wallet ID. Default value is "default".
    /// When the token was created, if the value of this field was set to "default", this field is optional.
    /// However, if a merchant-specific wallet ID was set, this field is required.
    /// If you do not sent this field or you send the incorrect value, Paidy will return an error.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("wallet_id")]
    [DataMember(Name = "wallet_id")]
    public string? WalletId { get; init; }


    /// <summary>
    /// Reason for the request.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("reason")]
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
        [JsonConverter(typeof(DeleteReasonCodeConverter))]
        [JsonInclude]
        [JsonPropertyName("code")]
        [DataMember(Name = "code")]
        public DeleteReasonCode Code { get; init; }


        /// <summary>
        /// Additional, more specific details about the reason.
        /// This field cannot be empty.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("description")]
        [DataMember(Name = "description")]
        public string Description { get; init; }
    }
    #endregion
#pragma warning restore CS8618
}
