// <copyright file="IContentExperimentsParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Interfaces.Parameters
{
    /// <summary>
    /// Interface with content experiments parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#experiments"/>
    /// </summary>
    public interface IContentExperimentsParameters : ICloneable<IContentExperimentsParameters>
    {
        /// <summary>
        /// Gets or sets the experiment id.
        /// </summary>
        string ExperimentId { get; set; }

        /// <summary>
        /// Gets or sets the experiment variant.
        /// </summary>
        string ExperimentVariant { get; set; }
    }
}
