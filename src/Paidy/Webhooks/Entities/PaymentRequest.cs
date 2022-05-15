using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Paidy.Internals;

namespace Paidy.Webhooks.Entities;



/// <summary>
/// Represents the Paidy payment webhook request object.
/// </summary>
/// <remarks>
/// Reference : <a href="https://paidy.com/docs/en/webhook.html"></a>
/// </remarks>
public sealed class PaymentRequest
{
    #region Properties
#pragma warning disable CS8618
    /// <summary>
    /// Paidy payment ID.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("payment_id")]
    [DataMember(Name = "payment_id")]
    public string PaymentId { get; private init; }


    /// <summary>
    /// Paidy capture ID.
    /// Only sent for capture and refund events.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("capture_id")]
    [DataMember(Name = "capture_id")]
    public string? CaptureId { get; private init; }


    /// <summary>
    /// Paidy only sends webhooks for payment events.
    /// Set to "payment".
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("event_type")]
    [DataMember(Name = "event_type")]
    public string EventType { get; private init; }


    /// <summary>
    /// Indicates both the type of payment event and the result of the request.
    /// </summary>
    [JsonConverter(typeof(PaymentEventConverter))]
    [JsonInclude]
    [JsonPropertyName("status")]
    [DataMember(Name = "status")]
    public PaymentEvent Status { get; private init; }


    /// <summary>
    /// Merchant’s own unique reference for the order.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("order_ref")]
    [DataMember(Name = "order_ref")]
    public string? OrderRef { get; private init; }


    /// <summary>
    /// Reason for the refund.
    /// Only sent for refund events.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("reason")]
    [DataMember(Name = "reason")]
    public string? Reason { get; private init; }


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
    public static PaymentRequest? From(ReadOnlySpan<byte> json)
        => JsonSerializer.Deserialize<PaymentRequest>(json);


    /// <summary>
    /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static PaymentRequest? From(string json)
        => JsonSerializer.Deserialize<PaymentRequest>(json);


    /// <summary>
    /// Reads one JSON value (including objects or arrays) from the provided reader into an instance of the type specified by a generic type parameter.
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static PaymentRequest? From(ref Utf8JsonReader reader)
        => JsonSerializer.Deserialize<PaymentRequest>(ref reader);


    /// <summary>
    /// Asynchronously reads the UTF-8 encoded text representing a single JSON value into an instance of a type specified by a generic type parameter.
    /// The stream will be read to completion.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<PaymentRequest?> FromAsync(Stream stream, CancellationToken cancellationToken = default)
        => JsonSerializer.DeserializeAsync<PaymentRequest>(stream, options: null, cancellationToken);
    #endregion
}
