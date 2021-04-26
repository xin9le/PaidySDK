using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Paidy.Internals;



namespace Paidy.Webhooks.Entities
{
    /// <summary>
    /// Represents the Paidy token webhook request object.
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/en/webhook.html"></a>
    /// </remarks>
    public sealed class TokenRequest
    {
        #region Properties
#pragma warning disable CS8618
        /// <summary>
        /// Paidy token ID, beginning with tok_.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("token_id")]
        [DataMember(Name = "token_id")]
        public string TokenId { get; private init; }


        /// <summary>
        /// Indicates both the type of event and the result of the request.
        /// </summary>
        [JsonConverter(typeof(TokenEventConverter))]
        [JsonInclude]
        [JsonPropertyName("status")]
        [DataMember(Name = "status")]
        public TokenEvent Status { get; private init; }


        /// <summary>
        /// Date and time in UTC that the event was created, displayed in ISO 8601 format.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("timestamp")]
        [DataMember(Name = "timestamp")]
        public DateTimeOffset Timestamp { get; private init; }
#pragma warning restore CS8618
        #endregion


        #region Methods
        /// <summary>
        /// Parses the UTF-8 encoded text representing a single JSON value into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TokenRequest? From(ReadOnlySpan<byte> json)
            => JsonSerializer.Deserialize<TokenRequest>(json);


        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TokenRequest? From(string json)
            => JsonSerializer.Deserialize<TokenRequest>(json);


        /// <summary>
        /// Reads one JSON value (including objects or arrays) from the provided reader into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static TokenRequest? From(ref Utf8JsonReader reader)
            => JsonSerializer.Deserialize<TokenRequest>(ref reader);


        /// <summary>
        /// Asynchronously reads the UTF-8 encoded text representing a single JSON value into an instance of a type specified by a generic type parameter.
        /// The stream will be read to completion.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static ValueTask<TokenRequest?> FromAsync(Stream stream, CancellationToken cancellationToken = default)
            => JsonSerializer.DeserializeAsync<TokenRequest>(stream, options: null, cancellationToken);
        #endregion
    }
}
