// <copyright file="IEcommerceParameters.cs" company="Kolky">
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
    public interface IEcommerceParameters : IHitParameters
    {
        /// <summary>
        /// Gets the transaction id.
        /// </summary>
        string TransactionId { get; }
    }
}
