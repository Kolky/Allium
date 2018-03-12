// <copyright file="IEnhancedEcommerceParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Interfaces.Parameters
{
    /// <summary>
    /// Interface with Enhanced E-Commerce parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#enhanced-ecomm"/>
    /// </summary>
    public interface IEnhancedEcommerceParameters : ICloneable<IEnhancedEcommerceParameters>
    {
        /// <summary>
        /// Gets or sets the product action.
        /// </summary>
        string ProductAction { get; set; }

        /// <summary>
        /// Gets or sets the coupon code.
        /// </summary>
        string CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the product action list.
        /// </summary>
        string ProductActionList { get; set; }

        /// <summary>
        /// Gets or sets the checkout step.
        /// </summary>
        string CheckoutStep { get; set; }

        /// <summary>
        /// Gets or sets the checkout step option.
        /// </summary>
        string CheckoutStepOption { get; set; }

        /// <summary>
        /// Gets or sets the promotion action.
        /// </summary>
        string PromotionAction { get; set; }

        /// <summary>
        /// Gets or sets the currency code. (ISO 4217)
        /// </summary>
        string CurrencyCode { get; set; }
    }
}
