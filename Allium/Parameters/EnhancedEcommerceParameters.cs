// <copyright file="EnhancedECommerceParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Parameters
{
    using System;
    using Allium.Interfaces.Parameters;
    using Allium.Parameters.Attributes;
    using Validation;

    /// <summary>
    /// Enhanced Parameters for E-Commerce.
    /// </summary>
    internal class EnhancedEcommerceParameters : IEnhancedEcommerceParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnhancedEcommerceParameters"/> class.
        /// </summary>
        public EnhancedEcommerceParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnhancedEcommerceParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected EnhancedEcommerceParameters(EnhancedEcommerceParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.ProductAction = copy.ProductAction;
            this.CouponCode = copy.CouponCode;
            this.ProductActionList = copy.ProductActionList;
            this.CheckoutStep = copy.CheckoutStep;
            this.CheckoutStepOption = copy.CheckoutStepOption;
            this.PromotionAction = copy.PromotionAction;
            this.CurrencyCode = copy.CurrencyCode;
        }

        /// <summary>
        /// Gets or sets the product action.
        /// </summary>
        [Parameter("pa")]
        public string ProductAction { get; set; }

        /// <summary>
        /// Gets or sets the coupon code.
        /// </summary>
        [Parameter("tcc")]
        public string CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the product action list.
        /// </summary>
        [Parameter("pal")]
        public string ProductActionList { get; set; }

        /// <summary>
        /// Gets or sets the checkout step.
        /// </summary>
        [Parameter("cos")]
        public string CheckoutStep { get; set; }

        /// <summary>
        /// Gets or sets the checkout step option.
        /// </summary>
        [Parameter("col")]
        public string CheckoutStepOption { get; set; }

        /// <summary>
        /// Gets or sets the promotion action.
        /// </summary>
        [Parameter("promoa")]
        public string PromotionAction { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        [Parameter("cu", MaxLength = 10)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public IEnhancedEcommerceParameters Clone()
        {
            return new EnhancedEcommerceParameters(this);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
