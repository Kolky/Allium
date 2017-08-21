// <copyright file="IAnalyticsClient.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Interfaces
{
    using System.Threading.Tasks;
    using Parameters;

    /// <summary>
    /// Interface for Google Analytics Client.
    /// </summary>
    internal interface IAnalyticsClient
    {
        /// <summary>
        /// Gets or sets a value indicating whether we are in debug mode.
        /// </summary>
        bool IsDebug { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we are using a secure connection.
        /// </summary>
        bool IsSecure { get; set; }

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        string UserAgent { get; }

        /// <summary>
        /// Gets the beacon url.
        /// </summary>
        string BeaconUrl { get; }

        /// <summary>
        /// Send parameters to Google Analytics.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> Send(IGeneralParameters parameters);
    }
}
