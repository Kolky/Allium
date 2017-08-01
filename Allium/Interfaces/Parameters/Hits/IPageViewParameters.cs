// <copyright file="IPageViewParameters.cs" company="Kolky">
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
    /// Interface with a subset of the content information parameters required for a pageview hit.
    /// For the hit type: <see cref="HitType.PageView"/>.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#content"/>
    /// </summary>
    public interface IPageViewParameters : IHitParameters
    {
        /// <summary>
        /// Gets or sets the document location url.
        /// </summary>
        string DocumentLocationUrl { get; set; }

        /// <summary>
        /// Gets or sets the document host name.
        /// </summary>
        string DocumentHostName { get; set; }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        string DocumentPath { get; set; }
    }
}
