// <copyright file="IEcommerceItemParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Interfaces.Parameters.Hits
{
    /// <summary>
    /// Interface with E-Commerce parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#ecomm"/>
    /// </summary>
    public interface IEcommerceItemParameters : IEcommerceParameters
    {
        /// <summary>
        /// Gets the item name.
        /// </summary>
        string ItemName { get; }

        /// <summary>
        /// Gets or sets the item price.
        /// </summary>
        double ItemPrice { get; set; }

        /// <summary>
        /// Gets or sets the item quantity.
        /// </summary>
        int ItemQuantity { get; set; }

        /// <summary>
        /// Gets or sets the item code.
        /// </summary>
        string ItemCode { get; set; }

        /// <summary>
        /// Gets or sets the item category.
        /// </summary>
        string ItemCategory { get; set; }
    }
}
