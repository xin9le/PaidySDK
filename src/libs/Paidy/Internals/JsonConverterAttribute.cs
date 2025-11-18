using System;
using System.Text.Json.Serialization;

namespace Paidy.Internals;



/// <summary>
/// Provides a <see cref="JsonConverterAttribute"/> that can be passed constructor arguments.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class JsonConverterAttribute<T>(params object?[] args)
    : JsonConverterAttribute()
{
    #region フィールド
    private readonly Type _customConverterType = typeof(T);
    private readonly object?[] _arguments = args;
    #endregion


    #region Overrides
    /// <inheritdoc/>
    public override JsonConverter? CreateConverter(Type typeToConvert)
        => (JsonConverter?)Activator.CreateInstance(this._customConverterType, this._arguments);
    #endregion
}
