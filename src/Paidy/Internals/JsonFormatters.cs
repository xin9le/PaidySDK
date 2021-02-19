using System;
using System.Net;
using Paidy.Payments.Entities;
using Paidy.Tokens.Entities;
using Paidy.Webhooks.Entities;
using Utf8Json;
using Utf8Json.Formatters;



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
    /// Converts from/to <see cref="TokenEvent"/>.
    /// </summary>
    internal sealed class TokenEventFormatter : IJsonFormatter<TokenEvent>
    {
        public TokenEvent Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var value = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);
            return value switch
            {
                "activate_success" => TokenEvent.ActivateSuccess,
                "suspend_success" => TokenEvent.SuspendSuccess,
                "resume_success" => TokenEvent.ResumeSuccess,
                "delete_success" => TokenEvent.DeleteSuccess,
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
        }


        public void Serialize(ref JsonWriter writer, TokenEvent value, IJsonFormatterResolver formatterResolver)
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



    /// <summary>
    /// Provides a formatter that treats whitespace same as null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class IgnoreWhiteSpaceFormatter<T> : IJsonFormatter<T?>
        where T : struct
    {
        #region Properties
        /// <summary>
        /// Gets internal formatter
        /// </summary>
        private IJsonFormatter<T> Formatter { get; }
        #endregion


        #region Constructors
        /// <inheritdoc/>
        public IgnoreWhiteSpaceFormatter(IJsonFormatter<T> formatter)
            => this.Formatter = formatter;


        /// <inheritdoc/>
        public IgnoreWhiteSpaceFormatter(Type formatterType, object[] formatterArguments)
        {
            try
            {
                var instance = Activator.CreateInstance(formatterType, formatterArguments);
                this.Formatter = (IJsonFormatter<T>)instance!;
            }
            catch (Exception ex)
            {
                var message = "Can not create formatter from JsonFormatterAttribute, check the target formatter is public and has constructor with right argument. FormatterType:" + formatterType.Name;
                throw new InvalidOperationException(message, ex);
            }
        }
        #endregion


        #region IJsonFormatter implementaions
        /// <inheritdoc/>
        public void Serialize(ref JsonWriter writer, T? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                this.Formatter.Serialize(ref writer, value.Value, formatterResolver);
            }
        }


        /// <inheritdoc/>
        public T? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            //--- if white-space, treat same as null.
            var beforeOffset = reader.GetCurrentOffsetUnsafe();
            var value = reader.ReadString();
            if (string.IsNullOrWhiteSpace(value))
                return null;

            //--- Restore the offset and read again.
            var afterOffset = reader.GetCurrentOffsetUnsafe();
            reader.AdvanceOffset(beforeOffset - afterOffset);
            return this.Formatter.Deserialize(ref reader, formatterResolver);
        }
        #endregion
    }



    /// <summary>
    /// Provides a ISO 8601 datetime formatter that treats whitespace same as null.
    /// </summary>
    internal sealed class IgnoreWhiteSpaceISO8601DateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset?>
    {
        #region Properties
        /// <summary>
        /// Gets internal formatter
        /// </summary>
        private IgnoreWhiteSpaceFormatter<DateTimeOffset> Formatter { get; }
        #endregion


        #region Constructors
        /// <inheritdoc/>
        public IgnoreWhiteSpaceISO8601DateTimeOffsetFormatter()
            => this.Formatter = new IgnoreWhiteSpaceFormatter<DateTimeOffset>(ISO8601DateTimeOffsetFormatter.Default);
        #endregion


        #region IJsonFormatter implementaions
        /// <inheritdoc/>
        public void Serialize(ref JsonWriter writer, DateTimeOffset? value, IJsonFormatterResolver formatterResolver)
            => this.Formatter.Serialize(ref writer, value, formatterResolver);


        /// <inheritdoc/>
        public DateTimeOffset? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            => this.Formatter.Deserialize(ref reader, formatterResolver);
        #endregion
    }
}
