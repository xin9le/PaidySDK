using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Paidy.Internals;



/// <summary>
/// Provides some caches for <see cref="JsonSerializerOptions"/>.
/// </summary>
internal static class JsonSerializerOptionsProvider
{
    #region Properties
    /// <summary>
    /// - <see cref="JsonSerializerDefaults.Web"/>
    /// </summary>
    public static JsonSerializerOptions Web { get; }


    /// <summary>
    /// - No unicode escaping<br/>
    /// - Doesn't write null value
    /// </summary>
    public static JsonSerializerOptions NoEscapeIgnoreNull { get; }
    #endregion


    #region Constructors
    /// <summary>
    /// 
    /// </summary>
    static JsonSerializerOptionsProvider()
    {
        Web = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        NoEscapeIgnoreNull = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }
    #endregion
}
