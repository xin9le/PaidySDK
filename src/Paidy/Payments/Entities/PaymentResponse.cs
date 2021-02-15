using System;
using System.Collections.Generic;
using System.Runtime.Serialization;



namespace Paidy.Payments.Entities
{
    /// <summary>
    /// Represents Paidy payment response object.
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/en/payments.html#status"></a>
    /// </remarks>
    public sealed class PaymentResponse
    {
#pragma warning disable CS8618
        #region Properties
        /// <summary>
        /// Payment ID.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; private init; }


        /// <summary>
        /// Payment data created time.
        /// </summary>
        [DataMember(Name = "created_at")]
        public DateTimeOffset CreatedAt { get; private init; }


        /// <summary>
        /// Payment data expiration time.
        /// </summary>
        [DataMember(Name = "expires_at")]
        public DateTimeOffset ExpiresAt { get; private init; }


        /// <summary>
        /// Total payment amount, including tax, shipping, and excluding any discounts.
        /// </summary>
        [DataMember(Name = "amount")]
        public decimal Amount { get; private init; }


        /// <summary>
        /// ISO 4217 currency code for this order; set to JPY.
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; private init; }


        /// <summary>
        /// Description for the payment.
        /// </summary>
        [DataMember(Name = "description")]
        public string? Description { get; private init; }


        /// <summary>
        /// Name of the store.
        /// This is displayed in the Checkout app header, at MyPaidy, and the Merchant Dashboard.
        /// </summary>
        [DataMember(Name = "store_name")]
        public string? StoreName { get; private init; }


        /// <summary>
        /// Test mode or not.
        /// </summary>
        [DataMember(Name = "test")]
        public bool Test { get; private init; }


        /// <summary>
        /// Payment status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; private init; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "tier")]
        public string Tier { get; private init; }


        /// <summary>
        /// <see cref="BuyerInfo"/> object containing details about the buyer.
        /// </summary>
        [DataMember(Name = "buyer")]
        public BuyerInfo Buyer { get; private init; }


        /// <summary>
        /// <see cref="OrderInfo"/> object containing details about the items being purchased.
        /// </summary>
        [DataMember(Name = "order")]
        public OrderInfo Order { get; private init; }


        /// <summary>
        /// <see cref="ShippingAddressInfo"/> object containing the address to which the goods are being shipped.
        /// </summary>
        [DataMember(Name = "shipping_address")]
        public ShippingAddressInfo ShippingAddress { get; private init; }


        /// <summary>
        /// Captured data collection.
        /// </summary>
        [DataMember(Name = "captures")]
        public IReadOnlyList<CaptureInfo> Captures { get; private init; }


        /// <summary>
        /// Refunded data collection.
        /// </summary>
        [DataMember(Name = "refunds")]
        public IReadOnlyList<RefundInfo> Refunds { get; private init; }


        /// <summary>
        /// Merchant-defined data about the object.
        /// This field is a key-value map, limited to 20 keys.
        /// The metadata field can be set in the merchant configuration and in the payload data.
        /// If it is set in both places, the values set in the payload will overwrite the values set in the merchant configuration.
        /// </summary>
        [DataMember(Name = "metadata")]
        public IReadOnlyDictionary<string, object>? Metadata { get; private init; }
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
            [DataMember(Name = "name1")]
            public string Name1 { get; private init; }


            /// <summary>
            /// Consumer's name in katakana.
            /// Family name and first name must be separated by a space, e.g., ヤマダ　タロウ.
            /// The space can be a Unicode U+0020 space or a U+3000 ideographic space.
            /// </summary>
            [DataMember(Name = "name2")]
            public string? Name2 { get; private init; }


            /// <summary>
            /// Consumer's email address.
            /// If this field is included in the payload, it will be pre-filled in the Paidy Checkout app when it launches.
            /// If this field are not included in the payload, the consumer must enter them during checkout.
            /// </summary>
            [DataMember(Name = "email")]
            public string? Email { get; private init; }


            /// <summary>
            /// Consumer's phone number, e.g., 09011112222.
            /// This should be a Japanese mobile phone where the consumer can receive text messages.
            /// If this field is included in the payload, it will be pre-filled in the Paidy Checkout app when it launches.
            /// If this field are not included in the payload, the consumer must enter them during checkout.
            /// </summary>
            [DataMember(Name = "phone")]
            public string? Phone { get; private init; }
        }


        /// <summary>
        /// Represents order object.
        /// </summary>
        public sealed class OrderInfo
        {
            /// <summary>
            /// Total tax charged on the order.
            /// </summary>
            [DataMember(Name = "tax")]
            public decimal? Tax { get; private init; }


            /// <summary>
            /// Total shipping charges for the order.
            /// </summary>
            [DataMember(Name = "shipping")]
            public decimal? Shipping { get; private init; }


            /// <summary>
            /// Merchant-defined order ID or reference.
            /// </summary>
            [DataMember(Name = "order_ref")]
            public string? OrderRef { get; private init; }


            /// <summary>
            /// Array of items objects representing all of the items in the order.
            /// If you want to offer consumers a discount, use this object to create a "discount order item", with the unit_price set to the negative value of the discount.
            /// </summary>
            [DataMember(Name = "items")]
            public IReadOnlyList<ItemInfo> Items { get; private init; }


            /// <summary>
            /// Order data updated time.
            /// </summary>
            [DataMember(Name = "updated_at")]
            public DateTimeOffset? UpdatedAt { get; private init; }
        }


        /// <summary>
        /// Represents item object.
        /// </summary>
        public sealed class ItemInfo
        {
            /// <summary>
            /// Merchant-defined product identifier.
            /// </summary>
            [DataMember(Name = "id")]
            public string? Id { get; private init; }


            /// <summary>
            /// Title of the order item (or discount/coupon).
            /// </summary>
            [DataMember(Name = "title")]
            public string? Title { get; private init; }


            /// <summary>
            /// Description of the item.
            /// </summary>
            [DataMember(Name = "description")]
            public string? Description { get; private init; }


            /// <summary>
            /// Price per unit for the item.
            /// </summary>
            [DataMember(Name = "unit_price")]
            public decimal UnitPrice { get; private init; }


            /// <summary>
            /// Quantity of the item added to the order.
            /// </summary>
            [DataMember(Name = "quantity")]
            public int Quantity { get; private init; }
        }


        /// <summary>
        /// Represents shipping address object.
        /// </summary>
        public sealed class ShippingAddressInfo
        {
            /// <summary>
            /// For Japanese addresses: building name, apartment number.
            /// </summary>
            [DataMember(Name = "line1")]
            public string? Line1 { get; private init; }


            /// <summary>
            /// For Japanese addresses: district, land number, land number extension.
            /// </summary>
            [DataMember(Name = "line2")]
            public string? Line2 { get; private init; }


            /// <summary>
            /// Name of city, municipality, or village.
            /// </summary>
            [DataMember(Name = "city")]
            public string? City { get; private init; }


            /// <summary>
            /// Prefecture
            /// </summary>
            [DataMember(Name = "state")]
            public string? State { get; private init; }


            /// <summary>
            /// Postal code; format is NNN-NNNN.
            /// </summary>
            [DataMember(Name = "zip")]
            public string Zip { get; private init; }
        }


        /// <summary>
        /// Represents capture object.
        /// </summary>
        public sealed class CaptureInfo
        {
            /// <summary>
            /// Capture ID.
            /// </summary>
            [DataMember(Name = "id")]
            public string Id { get; private init; }


            /// <summary>
            /// Capture data created time.
            /// </summary>
            [DataMember(Name = "created_at")]
            public DateTimeOffset CreatedAt { get; private init; }


            /// <summary>
            /// Total capture amount, including tax, shipping, and excluding any discounts.
            /// </summary>
            [DataMember(Name = "amount")]
            public decimal Amount { get; private init; }


            /// <summary>
            /// Total tax charged on the capture.
            /// </summary>
            [DataMember(Name = "tax")]
            public decimal? Tax { get; private init; }


            /// <summary>
            /// Total shipping charges for the capture.
            /// </summary>
            [DataMember(Name = "shipping")]
            public decimal? Shipping { get; private init; }


            /// <summary>
            /// Array of items objects representing all of the items in the capture.
            /// </summary>
            [DataMember(Name = "items")]
            public IReadOnlyList<ItemInfo> Items { get; private init; }


            /// <summary>
            /// Merchant-defined data about the object.
            /// This field is a key-value map, limited to 20 keys.
            /// </summary>
            [DataMember(Name = "metadata")]
            public IReadOnlyDictionary<string, object>? Metadata { get; private init; }
        }


        /// <summary>
        /// Represents refund object.
        /// </summary>
        public sealed class RefundInfo
        {
            /// <summary>
            /// Refund ID.
            /// </summary>
            [DataMember(Name = "id")]
            public string Id { get; private init; }


            /// <summary>
            /// Refund data created time.
            /// </summary>
            [DataMember(Name = "created_at")]
            public DateTimeOffset CreatedAt { get; private init; }


            /// <summary>
            /// Capture ID.
            /// </summary>
            [DataMember(Name = "capture_id")]
            public string CaptureId { get; private init; }


            /// <summary>
            /// Total refund amount, including tax, shipping, and excluding any discounts.
            /// Paidy uses this field to determine whether the request is for a partial refund or full refund.
            /// If no amount is specified, Paidy refunds the full payment amount for that capture.
            /// </summary>
            [DataMember(Name = "amount")]
            public decimal? Amount { get; private init; }


            /// <summary>
            /// Containing the reason for the refund.
            /// </summary>
            public string? reason { get; private init; }


            /// <summary>
            /// Merchant-defined data about the object.
            /// This field is a key-value map, limited to 20 keys.
            /// </summary>
            [DataMember(Name = "metadata")]
            public IReadOnlyDictionary<string, object>? Metadata { get; private init; }
        }
        #endregion
#pragma warning restore CS8618
    }
}
