// <copyright file="ItemHitParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Parameters.Hits
{
    using Allium.Enums;
    using Allium.Interfaces.Parameters;
    using Allium.Interfaces.Parameters.Hits;
    using Allium.Parameters.Attributes;

    /// <summary>
    /// Parameters for an E-Commerce item hit
    /// </summary>
    internal class ItemHitParameters : CloneableHitParameters<IEcommerceItemParameters>, IEcommerceItemParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="transactionId">transactionId</param>
        /// <param name="itemName">itemName</param>
        public ItemHitParameters(IGeneralParameters copy, string transactionId, string itemName)
            : base(copy)
        {
            this.TransactionId = transactionId;
            this.ItemName = itemName;
        }

        private ItemHitParameters(ItemHitParameters copy)
            : base(copy)
        {
            this.TransactionId = copy.TransactionId;
            this.ItemName = copy.ItemName;
            this.ItemPrice = copy.ItemPrice;
            this.ItemQuantity = copy.ItemQuantity;
            this.ItemCode = copy.ItemCode;
            this.ItemCategory = copy.ItemCategory;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.Item; }
        }

        /// <summary>
        /// Gets or sets the transaction id.
        /// </summary>
        [Parameter("ti", MaxLength = 500)]
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        [Parameter("in", MaxLength = 500)]
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets the item price.
        /// </summary>
        [Parameter("ip")]
        public double ItemPrice { get; set; }

        /// <summary>
        /// Gets or sets the item quantity.
        /// </summary>
        [Parameter("iq")]
        public int ItemQuantity { get; set; }

        /// <summary>
        /// Gets or sets the item code.
        /// </summary>
        [Parameter("ic", MaxLength = 500)]
        public string ItemCode { get; set; }

        /// <summary>
        /// Gets or sets the item category.
        /// </summary>
        [Parameter("iv", MaxLength = 500)]
        public string ItemCategory { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public override IEcommerceItemParameters Clone()
        {
            return new ItemHitParameters(this);
        }
    }
}
