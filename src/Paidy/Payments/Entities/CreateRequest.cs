﻿using System.Collections.Generic;
using System.Runtime.Serialization;



namespace Paidy.Payments.Entities
{
    /// <summary>
    /// Represents the Paidy payment create request object.
    /// </summary>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#2-2-create-a-payment"></a>
    /// </remarks>
    public sealed class CreateRequest
    {
#pragma warning disable CS8618
        #region Properties
        /// <summary>
        /// Paidy-generated token ID, beginning with tok_.
        /// </summary>
        [DataMember(Name = "token_id")]
        public string TokenId { get; init; }


        /// <summary>
        /// Total payment amount, including tax and shipping.
        /// </summary>
        [DataMember(Name = "amount")]
        public decimal Amount { get; init; }


        /// <summary>
        /// ISO 4217 currency code for this order.
        /// Set to JPY.
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; init; }


        /// <summary>
        /// Description for the payment.
        /// Currently not displayed at MyPaidy or the Merchant Dashboard.
        /// </summary>
        [DataMember(Name = "description")]
        public string? Description { get; init; }


        /// <summary>
        /// Merchant store name; this is displayed at MyPaidy and the Merchant Dashboard.
        /// </summary>
        [DataMember(Name = "store_name")]
        public string? StoreName { get; init; }


        /// <summary>
        /// Buyer_data object containing information about the consumer's purchasing history.
        /// </summary>
        [DataMember(Name = "buyer_data")]
        public BuyerInfo Buyer { get; init; }


        /// <summary>
        /// Order object.
        /// Order/cart details passed by the merchant.
        /// </summary>
        [DataMember(Name = "order")]
        public OrderInfo Order { get; init; }


        /// <summary>
        /// Merchant-defined data about the object.
        /// This field is a key-value map, limited to 20 keys.
        /// </summary>
        [DataMember(Name = "metadata")]
        public IDictionary<string, object>? Metadata { get; init; }


        /// <summary>
        /// Consumer's shipping address.
        /// </summary>
        [DataMember(Name = "shipping_address")]
        public ShippingAddressInfo ShippingAddress { get; init; }
        #endregion



        #region Internal Types
        /// <summary>
        /// Represents buyer data object.
        /// </summary>
        public sealed class BuyerInfo
        {
            /// <summary>
            /// Time in days since the consumer opened the account with the merchant.
            /// </summary>
            [DataMember(Name = "age")]
            public int Age { get; init; }


            /// <summary>
            /// Number of orders the consumer has made since signing up with the merchant, excluding canceled, rejected, or refunded transactions.
            /// Also excluding Paidy payments.
            /// </summary>
            [DataMember(Name = "order_count")]
            public int OrderCount { get; init; }


            /// <summary>
            /// Lifetime value.
            /// The total amount (in JPY) the consumer has ordered since signing up with the merchant, excluding canceled, rejected, or refunded transactions.
            /// Also excluding Paidy payments.
            /// </summary>
            [DataMember(Name = "ltv")]
            public decimal LifetimeValue { get; init; }


            /// <summary>
            /// Amount (in JPY) of the last order, excluding Paidy payments.
            /// </summary>
            [DataMember(Name = "last_order_amount")]
            public decimal LastOrderAmount { get; init; }


            /// <summary>
            /// Time in days since the last order, excluding Paidy payments.
            /// </summary>
            [DataMember(Name = "last_order_at")]
            public int LastOrderAt { get; init; }
        }


        /// <summary>
        /// Represents order object.
        /// </summary>
        public sealed class OrderInfo
        {
            /// <summary>
            /// Array of items object srepresenting all of the items in the order.
            /// If you want to offer consumers a discount, use this object to create a "discount order item", with the unit_price set to the negative value of the discount.
            /// </summary>
            [DataMember(Name = "items")]
            public IReadOnlyList<ItemInfo> Items { get; init; }


            /// <summary>
            /// Total tax charged on the order.
            /// </summary>
            [DataMember(Name = "tax")]
            public decimal? Tax { get; init; }


            /// <summary>
            /// Total shipping charges for the order.
            /// </summary>
            [DataMember(Name = "shipping")]
            public decimal? Shipping { get; init; }


            /// <summary>
            /// Merchant's order ID or reference.
            /// (In the Paidy API v2, this field is not required and does not need to be unique.)
            /// </summary>
            [DataMember(Name = "order_ref")]
            public string? OrderRef { get; init; }
        }


        /// <summary>
        /// Represents item object.
        /// </summary>
        public sealed class ItemInfo
        {
            /// <summary>
            /// Quantity of the item added to the order.
            /// </summary>
            [DataMember(Name = "quantity")]
            public int Quantity { get; init; }


            /// <summary>
            /// Merchant’s product identifier.
            /// This field is optional, but if it is sent, it will be displayed at the Merchant Dashboard and MyPaidy.
            /// </summary>
            [DataMember(Name = "id")]
            public string? Id { get; init; }


            /// <summary>
            /// Title of the order item (or discount/coupon).
            /// This field is optional, but we recommend sending it as part of the payload.
            /// If sent, it is shown at both the Merchant Dashboard and MyPaidy to identify the order item.
            /// If not sent, only the quantity and unit price will be displayed for the order item.
            /// </summary>
            [DataMember(Name = "title")]
            public string? Title { get; init; }


            /// <summary>
            /// Description for the product.
            /// Currently, this is not displayed at the Merchant Dashboard or MyPaidy.
            /// </summary>
            [DataMember(Name = "description")]
            public string? Description { get; init; }


            /// <summary>
            /// Price per unit for the item.
            /// If the order item is a discount or coupon, make sure the unit_price is set to a negative value, so that it will be subtracted from the total amount for the order.
            /// </summary>
            [DataMember(Name = "unit_price")]
            public decimal UnitPrice { get; init; }
        }


        /// <summary>
        /// Represents shipping address object.
        /// </summary>
        public sealed class ShippingAddressInfo
        {
            /// <summary>
            /// Building name, apartment number.
            /// </summary>
            [DataMember(Name = "line1")]
            public string? Line1 { get; init; }


            /// <summary>
            /// District, land number, land extension number.
            /// </summary>
            [DataMember(Name = "line2")]
            public string? Line2 { get; init; }


            /// <summary>
            /// Name of city, municipality, or village.
            /// </summary>
            [DataMember(Name = "city")]
            public string? City { get; init; }


            /// <summary>
            /// Prefecture.
            /// </summary>
            [DataMember(Name = "state")]
            public string? State { get; init; }


            /// <summary>
            /// Postal code; format is NNN-NNNN.
            /// </summary>
            [DataMember(Name = "zip")]
            public string Zip { get; init; }
        }
        #endregion
#pragma warning restore CS8618
    }
}
