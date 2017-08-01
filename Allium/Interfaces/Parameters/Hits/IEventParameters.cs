// <copyright file="IEventParameters.cs" company="Kolky">
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
    /// Interface with parameters for events. For the hit type: <see cref="HitType.Event"/>.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#events"/>
    /// </summary>
    public interface IEventParameters : IHitParameters
    {
        /// <summary>
        /// Gets or sets the event category.
        /// </summary>
        string EventCategory { get; set; }

        /// <summary>
        /// Gets or sets the event action.
        /// </summary>
        string EventAction { get; set; }

        /// <summary>
        /// Gets or sets the event label.
        /// </summary>
        string EventLabel { get; set; }

        /// <summary>
        /// Gets or sets the event value. Values must be non-negative.
        /// </summary>
        uint EventValue { get; set; }
    }
}
