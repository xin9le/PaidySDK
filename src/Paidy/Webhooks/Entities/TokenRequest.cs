using System;
using System.IO;
using System.Runtime.Serialization;
using Paidy.Internals;
using Utf8Json;
using Utf8Json.Resolvers;



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
        /// <summary>
        /// Paidy token ID, beginning with tok_.
        /// </summary>
        [DataMember(Name = "token_id")]
        public string TokenId { get; private init; }


        /// <summary>
        /// Indicates both the type of event and the result of the request.
        /// </summary>
        [DataMember(Name = "status")]
        [JsonFormatter(typeof(TokenEventFormatter))]
        public TokenEvent Status { get; private init; }


        /// <summary>
        /// Date and time in UTC that the event was created, displayed in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "timestamp")]
        public DateTimeOffset Timestamp { get; private init; }
        #endregion


        #region Constructors
#pragma warning disable CS8618
        /// <summary>
        /// Creates an instance.
        /// </summary>
        private TokenRequest()
        { }
#pragma warning restore CS8618
        #endregion


        #region Methods
        /// <summary>
        /// Creates an instance from a <see cref="byte"/>[] containing JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TokenRequest From(byte[] json)
            => JsonSerializer.Deserialize<TokenRequest>(json, StandardResolver.AllowPrivate);


        /// <summary>
        /// Creates an instance from a <see cref="string"/> containing JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TokenRequest From(string json)
            => JsonSerializer.Deserialize<TokenRequest>(json, StandardResolver.AllowPrivate);


        /// <summary>
        /// Creates an instance from a <see cref="Stream"/> containing JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TokenRequest From(Stream json)
            => JsonSerializer.Deserialize<TokenRequest>(json, StandardResolver.AllowPrivate);
        #endregion
    }
}
