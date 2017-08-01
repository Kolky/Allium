// <copyright file="ISystemInfoParameters.cs" company="Kolky">
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
    /// Interface with the system info parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#system"/>
    /// </summary>
    public interface ISystemInfoParameters : ICloneable<ISystemInfoParameters>
    {
        /// <summary>
        /// Gets or sets the screen resolution.
        /// </summary>
        string ScreenResolution { get; set; }

        /// <summary>
        /// Gets or sets the viewport size.
        /// </summary>
        string ViewportSize { get; set; }

        /// <summary>
        /// Gets or sets the document encoding.
        /// </summary>
        string DocumentEncoding { get; set; }

        /// <summary>
        /// Gets or sets the screen colors.
        /// </summary>
        string ScreenColors { get; set; }

        /// <summary>
        /// Gets or sets the user language.
        /// </summary>
        string UserLanguage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether java is enabled.
        /// </summary>
        bool? JavaEnabled { get; set; }

        /// <summary>
        /// Gets or sets the flash version.
        /// </summary>
        string FlashVersion { get; set; }
    }
}
