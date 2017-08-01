// <copyright file="HitType.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The type of hit.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#t"/>
    /// </summary>
    public enum HitType
    {
        /// <summary>
        /// Page view hit.
        /// </summary>
        PageView,

        /// <summary>
        /// Screen view hit.
        /// </summary>
        ScreenView,

        /// <summary>
        /// Event hit.
        /// </summary>
        Event,

        /// <summary>
        /// E-Commerce transaction hit.
        /// </summary>
        Transaction,

        /// <summary>
        /// E-Commerce item hit.
        /// </summary>
        Item,

        /// <summary>
        /// Social interaction hit.
        /// </summary>
        Social,

        /// <summary>
        /// Exception hit.
        /// </summary>
        Exception,

        /// <summary>
        /// Timing hit.
        /// </summary>
        Timing
    }
}
