// <copyright file="IAnalyticsTiming.cs" company="Kolky">
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
    using System.Threading.Tasks;
    using Parameters.Hits;

    /// <summary>
    /// Interface for a Google Analytics timing.
    /// </summary>
    public interface IAnalyticsTiming : IDisposable
    {
        /// <summary>
        /// Gets the parameters for this timing.
        /// </summary>
        ITimingParameters Parameters { get; }

        /// <summary>
        /// Gets the moment the timing was started.
        /// </summary>
        DateTime Started { get; }

        /// <summary>
        /// Gets the moment the timing was finished.
        /// </summary>
        DateTime? Finished { get; }

        /// <summary>
        /// Gets the time elapsed.
        /// </summary>
        TimeSpan? Elapsed { get; }

        /// <summary>
        /// Finish the timing measurement.
        /// NOTE: Sequential calls updates the finished value.
        /// </summary>
        void Finish();

        /// <summary>
        /// Finish the timing measurement and directly send the parameters.
        /// </summary>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> FinishAndSend();

        /// <summary>
        /// If the timing is finished, send the parameters.
        /// </summary>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> Send();
    }
}
