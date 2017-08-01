// <copyright file="ISocialParameters.cs" company="Kolky">
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
    /// Interface with the social interactions parameters.  For the hit type: <see cref="HitType.Social"/>.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#social"/>
    /// </summary>
    public interface ISocialParameters : IHitParameters
    {
        /// <summary>
        /// Gets or sets the social network.
        /// </summary>
        string SocialNetwork { get; set; }

        /// <summary>
        /// Gets or sets the social action.
        /// </summary>
        string SocialAction { get; set; }

        /// <summary>
        /// Gets or sets the social action target. Typically a url.
        /// </summary>
        string SocialActionTarget { get; set; }
    }
}
