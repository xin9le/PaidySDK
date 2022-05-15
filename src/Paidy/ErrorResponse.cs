using System.Net;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Paidy;



/// <summary>
/// Represents error object.
/// </summary>
public sealed class ErrorResponse
{
#pragma warning disable CS8618
    /// <summary>
    /// Internal error code.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("reference")]
    [DataMember(Name = "reference")]
    public string Reference { get; private init; }


    /// <summary>
    /// HTTP response code.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("status")]
    [DataMember(Name = "status")]
    public HttpStatusCode Status { get; private init; }


    /// <summary>
    /// Code indicating the kind of error that occurred.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("code")]
    [DataMember(Name = "code")]
    public string Code { get; private init; }


    /// <summary>
    /// Text version of the error code.
    /// This field can sometimes provide the name of the field with which there is an issue.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("title")]
    [DataMember(Name = "title")]
    public string Title { get; private init; }


    /// <summary>
    /// Description providing more details about the error.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("description")]
    [DataMember(Name = "description")]
    public string Description { get; private init; }
#pragma warning restore CS8618
}
