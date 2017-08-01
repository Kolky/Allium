// <copyright file="IAppTrackingParameters.cs" company="Kolky">
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
    /// Interface with app tracking parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#apptracking"/>
    /// </summary>
    public interface IAppTrackingParameters : ICloneable<IAppTrackingParameters>
    {
        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets te application identifier.
        /// </summary>
        string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        string ApplicationVersion { get; set; }

        /// <summary>
        /// Gets or sets the application installer identifier.
        /// </summary>
        string ApplicationInstallerId { get; set; }
    }
}
