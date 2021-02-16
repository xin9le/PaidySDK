using System;
using System.Net;
using Paidy.Payments.Entities;
using Paidy.Webhooks.Entities;
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



    /// <summary>
    /// Converts from/to <see cref="PaymentEvent"/>.
    /// </summary>
    internal sealed class PaymentEventFormatter : IJsonFormatter<PaymentEvent>
    {
        public PaymentEvent Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var value = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);
            return value switch
            {
                "authorize_success" => PaymentEvent.AuthorizeSuccess,
                "capture_success" => PaymentEvent.CaptureSuccess,
                "refund_success" => PaymentEvent.RefundSuccess,
                "update_success" => PaymentEvent.UpdateSuccess,
                "close_success" => PaymentEvent.CloseSuccess,
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
        }


        public void Serialize(ref JsonWriter writer, PaymentEvent value, IJsonFormatterResolver formatterResolver)
            => throw new NotImplementedException();
    }



    /// <summary>
    /// Converts from/to <see cref="HttpStatusCode"/>.
    /// </summary>
    internal sealed class HttpStatusCodeFormatter : IJsonFormatter<HttpStatusCode>
    {
        public HttpStatusCode Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var value = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);
            return (HttpStatusCode)int.Parse(value);
        }


        public void Serialize(ref JsonWriter writer, HttpStatusCode value, IJsonFormatterResolver formatterResolver)
            => throw new NotImplementedException();
    }
}
