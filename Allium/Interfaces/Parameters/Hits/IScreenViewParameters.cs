// <copyright file="IScreenViewParameters.cs" company="Kolky">
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
    using Enums;

    /// <summary>
    /// Interface with a subset of the content information parameters required for a screenview hit.
    /// For the hit type: <see cref="HitType.ScreenView"/>.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#content"/>
    /// </summary>
    public interface IScreenViewParameters : IHitParameters
    {
        /// <summary>
        /// Gets or sets the screen name.
        /// </summary>
        string ScreenName { get; set; }
    }
}
