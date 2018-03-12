// <copyright file="ICloneableHitParameters.cs" company="Kolky">
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
    /// Interface with parameters for a hit.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#hit"/>
    /// </summary>
    /// <typeparam name="T">Type of cloneable parameters.</typeparam>
    public interface ICloneableHitParameters<T> : ICloneableGeneralParameters<T>
    {
        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        HitType HitType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether a hit must be considered non-interactive.
        /// </summary>
        bool? NonInteractionHit { get; set; }
    }
}
