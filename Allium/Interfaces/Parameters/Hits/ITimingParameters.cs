// <copyright file="ITimingParameters.cs" company="Kolky">
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
    /// Interface with the timing parameters. For the hit type: <see cref="HitType.Timing"/>.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#timing"/>
    /// </summary>
    public interface ITimingParameters : ICloneableHitParameters<ITimingParameters>
    {
        /// <summary>
        /// Gets or sets the user timing category.
        /// </summary>
        string UserTimingCategory { get; set; }

        /// <summary>
        /// Gets or sets the user timing variable name.
        /// </summary>
        string UserTimingVariableName { get; set; }

        /// <summary>
        /// Gets or sets the user timing time.
        /// </summary>
        int UserTimingTime { get; set; }

        /// <summary>
        /// Gets or sets the user timing label.
        /// </summary>
        string UserTimingLabel { get; set; }

        /// <summary>
        /// Gets or sets the page load time.
        /// </summary>
        int PageLoadTime { get; set; }

        /// <summary>
        /// Gets or sets the dns time.
        /// </summary>
        int DnsTime { get; set; }

        /// <summary>
        /// Gets or sets the page download time.
        /// </summary>
        int PageDownloadTime { get; set; }

        /// <summary>
        /// Gets or sets the redirect response time.
        /// </summary>
        int RedirectResponseTime { get; set; }

        /// <summary>
        /// Gets or sets the tcp connect time.
        /// </summary>
        int TcpConnectTime { get; set; }

        /// <summary>
        /// Gets or sets the server response time.
        /// </summary>
        int ServerResponseTime { get; set; }

        /// <summary>
        /// Gets or sets the dom interactive time.
        /// </summary>
        int DomInteractiveTime { get; set; }

        /// <summary>
        /// Gets or sets the content load time.
        /// </summary>
        int ContentLoadTime { get; set; }
    }
}
