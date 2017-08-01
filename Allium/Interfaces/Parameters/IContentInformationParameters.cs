// <copyright file="IContentInformationParameters.cs" company="Kolky">
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
    using System.Collections.Generic;

    /// <summary>
    /// Interface with the content information parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#content"/>
    /// </summary>
    public interface IContentInformationParameters : ICloneable<IContentInformationParameters>
    {
        /// <summary>
        /// Gets or sets the document title.
        /// </summary>
        string DocumentTitle { get; set; }

        /// <summary>
        /// Gets the content groups.
        /// </summary>
        IList<string> ContentGroups { get; }

        /// <summary>
        /// Gets or sets the link id.
        /// </summary>
        string LinkId { get; set; }
    }
}
