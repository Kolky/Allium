// <copyright file="IAnalyticsEvent.cs" company="Kolky">
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
    using Parameters.Hits;

    /// <summary>
    /// Interface for an Analytics Event.
    /// </summary>
    public interface IAnalyticsEvent
    {
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        IEventParameters Parameters { get; }

        /// <summary>
        /// Send the parameters.
        /// </summary>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> Send();
    }
}
