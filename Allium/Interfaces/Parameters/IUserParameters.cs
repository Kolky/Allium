// <copyright file="IUserParameters.cs" company="Kolky">
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
    using System;

    /// <summary>
    /// Interface with the user parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#user"/>
    /// </summary>
    public interface IUserParameters : ICloneable<IUserParameters>
    {
        /// <summary>
        /// Gets or sets the anonymous identification of a particular user, device, or browser instance.
        /// </summary>
        Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        string UserId { get; set; }
    }
}
