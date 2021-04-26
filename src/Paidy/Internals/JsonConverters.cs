using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Paidy.Payments.Entities;
using Paidy.Tokens.Entities;
using Paidy.Webhooks.Entities;



namespace Paidy.Internals
{
    /// <summary>
    /// Converts from/to <see cref="PaymentStatus"/>.
    /// </summary>
    internal sealed class PaymentStatusConverter : JsonConverter<PaymentStatus>
    {
        #region Overrides
        /// <inheritdoc/>
        public override PaymentStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "authorized" => PaymentStatus.Authorized,
                "closed" => PaymentStatus.Closed,
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
        }


        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, PaymentStatus value, JsonSerializerOptions options)
            => throw new NotImplementedException();
        #endregion
    }



    /// <summary>
    /// Converts from/to <see cref="PaymentEvent"/>.
    /// </summary>
    internal sealed class PaymentEventConverter : JsonConverter<PaymentEvent>
    {
        #region Overrides
        /// <inheritdoc/>
        public override PaymentEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
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


        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, PaymentEvent value, JsonSerializerOptions options)
            => throw new NotImplementedException();
        #endregion
    }



    /// <summary>
    /// Converts from/to <see cref="TokenStatus"/>.
    /// </summary>
    internal sealed class TokenStatusConverter : JsonConverter<TokenStatus>
    {
        #region Overrides
        /// <inheritdoc/>
        public override TokenStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "active" => TokenStatus.Active,
                "suspended" => TokenStatus.Suspended,
                "deleted" => TokenStatus.Deleted,
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
        }


        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, TokenStatus value, JsonSerializerOptions options)
            => throw new NotImplementedException();
        #endregion
    }



    /// <summary>
    /// Converts from/to <see cref="TokenEvent"/>.
    /// </summary>
    internal sealed class TokenEventConverter : JsonConverter<TokenEvent>
    {
        #region Overrides
        /// <inheritdoc/>
        public override TokenEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "activate_success" => TokenEvent.ActivateSuccess,
                "suspend_success" => TokenEvent.SuspendSuccess,
                "resume_success" => TokenEvent.ResumeSuccess,
                "delete_success" => TokenEvent.DeleteSuccess,
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
        }


        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, TokenEvent value, JsonSerializerOptions options)
            => throw new NotImplementedException();
        #endregion
    }



    /// <summary>
    /// Converts from/to <see cref="SuspendReasonCode"/>.
    /// </summary>
    internal sealed class SuspendReasonCodeConverter : JsonConverter<SuspendReasonCode>
    {
        #region Overrides
        /// <inheritdoc/>
        public override SuspendReasonCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();


        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, SuspendReasonCode value, JsonSerializerOptions options)
        {
            var text = value switch
            {
                SuspendReasonCode.ConsumerRequested => "consumer.requested",
                SuspendReasonCode.MerchantRequested => "merchant.requested",
                SuspendReasonCode.FraudDetected => "fraud.detected",
                SuspendReasonCode.General => "general",
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
            writer.WriteStringValue(text);
        }
        #endregion
    }



    /// <summary>
    /// Converts from/to <see cref="ResumeReasonCode"/>.
    /// </summary>
    internal sealed class ResumeReasonCodeConverter : JsonConverter<ResumeReasonCode>
    {
        #region Overrides
        /// <inheritdoc/>
        public override ResumeReasonCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();


        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, ResumeReasonCode value, JsonSerializerOptions options)
        {
            var text = value switch
            {
                ResumeReasonCode.ConsumerRequested => "consumer.requested",
                ResumeReasonCode.MerchantRequested => "merchant.requested",
                ResumeReasonCode.General => "general",
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
            writer.WriteStringValue(text);
        }
        #endregion
    }



    /// <summary>
    /// Converts from/to <see cref="DeleteReasonCode"/>.
    /// </summary>
    internal sealed class DeleteReasonCodeConverter : JsonConverter<DeleteReasonCode>
    {
        #region Overrides
        /// <inheritdoc/>
        public override DeleteReasonCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();


        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, DeleteReasonCode value, JsonSerializerOptions options)
        {
            var text = value switch
            {
                DeleteReasonCode.ConsumerRequested => "consumer.requested",
                DeleteReasonCode.SubscriptionExpired => "subscription.expired",
                DeleteReasonCode.MerchantRequested => "merchant.requested",
                DeleteReasonCode.FraudDetected => "fraud.detected",
                DeleteReasonCode.General => "general",
                _ => throw new NotSupportedException($"Unexpected values are set. | Value : {value}"),
            };
            writer.WriteStringValue(text);
        }
        #endregion
    }
}
