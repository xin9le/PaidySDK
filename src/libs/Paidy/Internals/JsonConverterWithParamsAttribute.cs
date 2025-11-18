using System;
using System.Text.Json.Serialization;

namespace Paidy.Internals;



/// <summary>
/// Provides a <see cref="JsonConverterAttribute"/> that can be passed constructor arguments.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
internal sealed class JsonConverterWithParamsAttribute : JsonConverterAttribute
{
    #region Properties
    /// <summary>
    /// Gets the type of <see cref="JsonConverter"/>.
    /// </summary>
    private Type CustomeConverterType { get; }


    /// <summary>
    /// Gets constructor arguments.
    /// </summary>
    private object?[] Arguments { get; }
    #endregion


    #region Constructors
    /// <summary>
    /// Creates instance.
    /// </summary>
    /// <param name="converterType"></param>
    /// <param name="args">Constructor arguments</param>
    public JsonConverterWithParamsAttribute(Type converterType, params object?[] args)
        : base()
    {
        this.CustomeConverterType = converterType;
        this.Arguments = args;
    }
    #endregion


    #region Overrides
    /// <inheritdoc/>
    public override JsonConverter? CreateConverter(Type typeToConvert)
        => (JsonConverter?)Activator.CreateInstance(this.CustomeConverterType, this.Arguments);
    #endregion
}
