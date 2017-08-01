// <copyright file="IAnalyticsResult.cs" company="Kolky">
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
    using System;

    /// <summary>
    /// Interface containing the Analytics Results.
    /// </summary>
    public interface IAnalyticsResult
    {
        /// <summary>
        /// Gets a value indicating whether the analytics item was successfully send.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the exception if anything failed.
        /// </summary>
        Exception Exception { get; }
    }
}
