// <copyright file="IExceptionParameters.cs" company="Kolky">
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
    /// Interface with exceptions parameters. For the hit type: <see cref="HitType.Exception"/>.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#exception"/>
    /// </summary>
    public interface IExceptionParameters : IHitParameters
    {
        /// <summary>
        /// Gets or sets the exception description.
        /// </summary>
        string ExceptionDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the exception was fatal.
        /// </summary>
        bool? IsExceptionFatal { get; set; }
    }
}
