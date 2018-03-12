// <copyright file="IAnalyticsHit.cs" company="Kolky">
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
    using Allium.Interfaces.Parameters.Hits;

    /// <summary>
    /// Interface for an Analytics Hit, to fill more parameters before sending.
    /// </summary>
    /// <typeparam name="T">Type of Hit Parameters</typeparam>
    public interface IAnalyticsHit<T>
        where T : ICloneableHitParameters<T>
    {
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        T Parameters { get; }

        /// <summary>
        /// Send the parameters.
        /// </summary>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> Send();
    }
}
