using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Paidy.Internals;

namespace Paidy.Payments.Entities;



/// <summary>
/// Represents the Paidy payment response object.
/// </summary>
/// <remarks>
/// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-1-payment-object"></a>
/// </remarks>
public sealed class PaymentResponse
{
#pragma warning disable CS8618
    #region Properties
    /// <summary>
    /// Unique identifier for the payment.
    /// All payment IDs begin with pay_.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("id")]
    [DataMember(Name = "id")]
    public string Id { get; private init; }


    /// <summary>
    /// Date and time the payment was created, in UTC, and displayed in ISO 8601 datetime format.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("created_at")]
    [DataMember(Name = "created_at")]
    public DateTimeOffset CreatedAt { get; private init; }


    /// <summary>
    /// Date and time the payment expires, in UTC, and displayed in ISO 8601 datetime format.
    /// You must capture the payment before this datetime.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("expires_at")]
    [DataMember(Name = "expires_at")]
    public DateTimeOffset ExpiresAt { get; private init; }


    /// <summary>
    /// Total payment amount, including tax, shipping, and excluding any discounts.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("amount")]
    [DataMember(Name = "amount")]
    public decimal Amount { get; private init; }


    /// <summary>
    /// ISO 4217 currency code for the payment amount; set to JPY.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("currency")]
    [DataMember(Name = "currency")]
    public string Currency { get; private init; }


    /// <summary>
    /// Description for the payment.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("description")]
    [DataMember(Name = "description")]
    public string? Description { get; private init; }


    /// <summary>
    /// Merchant store name.
    /// This field is displayed at both MyPaidy and the Merchant Dashboard.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("store_name")]
    [DataMember(Name = "store_name")]
    public string? StoreName { get; private init; }


    /// <summary>
    /// Indicates whether this is a test payment (created using a test API key).
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("test")]
    [DataMember(Name = "test")]
    public bool Test { get; private init; }


    /// <summary>
    /// Current status of this payment object.
    /// A payment that was successfully authorized has a status of AUTHORIZED.
    /// A payment that was successfully captured, closed, or canceled has a status of CLOSED.
    /// </summary>
    [JsonConverter(typeof(PaymentStatusConverter))]
    [JsonInclude]
    [JsonPropertyName("status")]
    [DataMember(Name = "status")]
    public PaymentStatus Status { get; private init; }


    /// <summary>
    /// Paidy-assigned payment type.
    /// The default value is "classic".
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("tier")]
    [DataMember(Name = "tier")]
    public string Tier { get; private init; }


    /// <summary>
    /// Buyer object.
    /// Consumer's name and contact information.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("buyer")]
    [DataMember(Name = "buyer")]
    public BuyerInfo Buyer { get; private init; }


    /// <summary>
    /// Order object.
    /// Order/cart details passed by the merchant.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("order")]
    [DataMember(Name = "order")]
    public OrderInfo Order { get; private init; }


    /// <summary>
    /// Shipping address object.
    /// Shipping address for the order.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("shipping_address")]
    [DataMember(Name = "shipping_address")]
    public ShippingAddressInfo ShippingAddress { get; private init; }


    /// <summary>
    /// Captures object.
    /// Array of objects representing the captured payments.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("captures")]
    [DataMember(Name = "captures")]
    public IReadOnlyList<CaptureInfo> Captures { get; private init; }


    /// <summary>
    /// Refunds object.
    /// Array of objects representing the refunded payments.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("refunds")]
    [DataMember(Name = "refunds")]
    public IReadOnlyList<RefundInfo> Refunds { get; private init; }


    /// <summary>
    /// Merchant-defined data about the object.
    /// This field is a key-value map, limited to 20 keys.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("metadata")]
    [DataMember(Name = "metadata")]
    public IReadOnlyDictionary<string, object> Metadata { get; private init; }
    #endregion



    #region Internal Types
    /// <summary>
    /// Represents buyer object.
    /// </summary>
    public sealed class BuyerInfo
    {
        /// <summary>
        /// Consumer's name in kanji.
        /// Family name and first name must be separated by a space, e.g., 山田　太郎.
        /// The space can be a Unicode U+0020 space or a U+3000 ideographic space.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("name1")]
        [DataMember(Name = "name1")]
        public string Name1 { get; private init; }


        /// <summary>
        /// Consumer's name in katakana.
        /// Family name and first name must be separated by a space, e.g., ヤマダ　タロウ.
        /// The space can be a Unicode U+0020 space or a U+3000 ideographic space.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("name2")]
        [DataMember(Name = "name2")]
        public string? Name2 { get; private init; }


        /// <summary>
        /// Consumer's email address.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("email")]
        [DataMember(Name = "email")]
        public string? Email { get; private init; }


        /// <summary>
        /// Consumer's phone number, e.g., 09011112222.
        /// This should be a Japanese mobile phone where the consumer can receive text messages.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("phone")]
        [DataMember(Name = "phone")]
        public string? Phone { get; private init; }
    }


    /// <summary>
    /// Represents order object.
    /// </summary>
    public sealed class OrderInfo
    {
        /// <summary>
        /// Items object.
        /// Array of objects representing the order items in this payment.
        /// If you want to offer consumers a discount, use this object to create a "discount order item", with the unit_price set to the negative value of the discount.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("items")]
        [DataMember(Name = "items")]
        public IReadOnlyList<ItemInfo> Items { get; private init; }


        /// <summary>
        /// Total tax for the order.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("tax")]
        [DataMember(Name = "tax")]
        public decimal? Tax { get; private init; }


        /// <summary>
        /// Total shipping cost for the order.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("shipping")]
        [DataMember(Name = "shipping")]
        public decimal? Shipping { get; private init; }


        /// <summary>
        /// Merchant-assigned order or cart ID.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("order_ref")]
        [DataMember(Name = "order_ref")]
        public string? OrderRef { get; private init; }


        /// <summary>
        /// Time the order was last updated, in UTC, and displayed in ISO 8601 datetime format.
        /// </summary>
        [JsonConverterWithParams(typeof(NullableDateTimeOffsetConverter), true)]
        [JsonInclude]
        [JsonPropertyName("updated_at")]
        [DataMember(Name = "updated_at")]
        public DateTimeOffset? UpdatedAt { get; private init; }
    }


    /// <summary>
    /// Represents item object.
    /// </summary>
    public sealed class ItemInfo
    {
        /// <summary>
        /// Merchant’s product identifier.
        /// This field is optional, but if it is sent, it will be displayed at the Merchant Dashboard and MyPaidy.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("id")]
        [DataMember(Name = "id")]
        public string? Id { get; private init; }


        /// <summary>
        /// Name of the product.
        /// This field is optional, but we recommend sending it as part of the payload.
        /// If sent, it is shown at both the Merchant Dashboard and MyPaidy to identify the order item.
        /// If not sent, only the quantity and unit price will be displayed for the order item.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("title")]
        [DataMember(Name = "title")]
        public string? Title { get; private init; }


        /// <summary>
        /// Description for the product.
        /// Currently, this is not displayed at the Merchant Dashboard or MyPaidy.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("description")]
        [DataMember(Name = "description")]
        public string? Description { get; private init; }


        /// <summary>
        /// Price per unit of the product.
        /// The unit_price can be a negative value to represent a discount.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("unit_price")]
        [DataMember(Name = "unit_price")]
        public decimal UnitPrice { get; private init; }


        /// <summary>
        /// Quantity of the product ordered.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("quantity")]
        [DataMember(Name = "quantity")]
        public int Quantity { get; private init; }
    }


    /// <summary>
    /// Represents shipping address object.
    /// </summary>
    public sealed class ShippingAddressInfo
    {
        /// <summary>
        /// Building name, apartment number.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("line1")]
        [DataMember(Name = "line1")]
        public string? Line1 { get; private init; }


        /// <summary>
        /// District, land number, land extension number.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("line2")]
        [DataMember(Name = "line2")]
        public string? Line2 { get; private init; }


        /// <summary>
        /// Name of city, municipality, or village.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("city")]
        [DataMember(Name = "city")]
        public string? City { get; private init; }


        /// <summary>
        /// Prefecture.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("state")]
        [DataMember(Name = "state")]
        public string? State { get; private init; }


        /// <summary>
        /// Postal code; format is NNN-NNNN.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("zip")]
        [DataMember(Name = "zip")]
        public string Zip { get; private init; }
    }


    /// <summary>
    /// Represents capture object.
    /// </summary>
    public sealed class CaptureInfo
    {
        /// <summary>
        /// Unique capture ID, assigned by Paidy.
        /// All capture IDs begin with cap_.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("id")]
        [DataMember(Name = "id")]
        public string Id { get; private init; }


        /// <summary>
        /// Date and time the capture was created, in UTC, and displayed in ISO 8601 datetime format.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("created_at")]
        [DataMember(Name = "created_at")]
        public DateTimeOffset CreatedAt { get; private init; }


        /// <summary>
        /// Amount captured.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("amount")]
        [DataMember(Name = "amount")]
        public decimal Amount { get; private init; }


        /// <summary>
        /// Tax amount captured.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("tax")]
        [DataMember(Name = "tax")]
        public decimal? Tax { get; private init; }


        /// <summary>
        /// Shipping cost captured.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("shipping")]
        [DataMember(Name = "shipping")]
        public decimal? Shipping { get; private init; }


        /// <summary>
        /// Array of objects representing the order items being captured.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("items")]
        [DataMember(Name = "items")]
        public IReadOnlyList<ItemInfo> Items { get; private init; }


        /// <summary>
        /// Merchant-defined data about the capture object.
        /// This field is a key-value map, limited to 20 keys.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("metadata")]
        [DataMember(Name = "metadata")]
        public IReadOnlyDictionary<string, object> Metadata { get; private init; }
    }


    /// <summary>
    /// Represents refund object.
    /// </summary>
    public sealed class RefundInfo
    {
        /// <summary>
        /// Unique refund ID, assigned by Paidy.
        /// All refund IDs begin with ref_.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("id")]
        [DataMember(Name = "id")]
        public string Id { get; private init; }


        /// <summary>
        /// Date and time the refund was created, in UTC, and displayed in ISO 8601 datetime format.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("created_at")]
        [DataMember(Name = "created_at")]
        public DateTimeOffset CreatedAt { get; private init; }


        /// <summary>
        /// Capture ID of the items being refunded.
        /// A refund must map to a capture.
        /// All capture IDs begin with cap_.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("capture_id")]
        [DataMember(Name = "capture_id")]
        public string CaptureId { get; private init; }


        /// <summary>
        /// Amount refunded.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("amount")]
        [DataMember(Name = "amount")]
        public decimal? Amount { get; private init; }


        /// <summary>
        /// Reason for the refund.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("reason")]
        [DataMember(Name = "reason")]
        public string? Reason { get; private init; }


        /// <summary>
        /// Merchant-defined data about the refund.
        /// This field is a key-value map, limited to 20 keys.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("metadata")]
        [DataMember(Name = "metadata")]
        public IReadOnlyDictionary<string, object> Metadata { get; private init; }
    }
    #endregion
#pragma warning restore CS8618
}
