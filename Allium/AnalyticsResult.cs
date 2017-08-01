// <copyright file="AnalyticsResult.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium
{
    using System;
    using Allium.Interfaces;

    /// <summary>
    /// Analytics result.
    /// </summary>
    internal class AnalyticsResult : IAnalyticsResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsResult"/> class.
        /// </summary>
        /// <param name="success">success</param>
        /// <param name="exception">exception</param>
        public AnalyticsResult(bool success, Exception exception)
        {
            this.Success = success;
            this.Exception = exception;
        }

        /// <summary>
        /// Gets a value indicating whether the analytics item was successfully send.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the exception if anything failed.
        /// </summary>
        public Exception Exception { get; private set; }
    }
}
