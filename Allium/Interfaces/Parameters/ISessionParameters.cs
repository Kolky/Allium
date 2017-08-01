// <copyright file="ISessionParameters.cs" company="Kolky">
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
    using System.Net;
    using Enums;

    /// <summary>
    /// Interface with the session parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#session"/>
    /// </summary>
    public interface ISessionParameters : ICloneable<ISessionParameters>
    {
        /// <summary>
        /// Gets or sets the session control.
        /// </summary>
        SessionControl? SessionControl { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the user.
        /// </summary>
        IPAddress IPOverride { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        string UserAgentOverride { get; set; }

        /// <summary>
        /// Gets or sets the geographical location of the user.
        /// <a href="http://developers.google.com/analytics/devguides/collection/protocol/v1/geoid"/>
        /// </summary>
        string GeographicalOverride { get; set; }
    }
}
