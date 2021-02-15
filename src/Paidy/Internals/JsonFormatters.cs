using System;
using Paidy.Payments.Entities;
using Utf8Json;



namespace Paidy.Internals
{
    /// <summary>
    /// Converts from/to <see cref="PaymentStatus"/>.
    /// </summary>
    internal sealed class PaymentStatusFormatter : IJsonFormatter<PaymentStatus>
    {
        public PaymentStatus Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var value = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);
            return value switch
            {
                "authorized" => PaymentStatus.Authorized,
                "closed" => PaymentStatus.Closed,
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
        }


        public void Serialize(ref JsonWriter writer, PaymentStatus value, IJsonFormatterResolver formatterResolver)
            => throw new NotImplementedException();
    }
}
