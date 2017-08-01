﻿// <copyright file="IAnalyticsClient.cs" company="Kolky">
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
        /// Send parameters to Google Analytics.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> Send(IGeneralParameters parameters);
    }
}
