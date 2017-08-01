// <copyright file="SystemInfoParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Parameters
{
    using System;
    using Attributes;
    using Interfaces.Parameters;
    using Validation;

    /// <summary>
    /// Parameters for system info.
    /// </summary>
    internal class SystemInfoParameters : ISystemInfoParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemInfoParameters"/> class.
        /// </summary>
        public SystemInfoParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemInfoParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected SystemInfoParameters(SystemInfoParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.ScreenResolution = copy.ScreenResolution;
            this.ViewportSize = copy.ViewportSize;
            this.DocumentEncoding = copy.DocumentEncoding;
            this.ScreenColors = copy.ScreenColors;
            this.UserLanguage = copy.UserLanguage;
            this.JavaEnabled = copy.JavaEnabled;
            this.FlashVersion = copy.FlashVersion;
        }

        /// <summary>
        /// Gets or sets the screen resolution.
        /// </summary>
        [Parameter("sr", MaxLength = 20)]
        public string ScreenResolution { get; set; }

        /// <summary>
        /// Gets or sets the viewport size.
        /// </summary>
        [Parameter("vp", MaxLength = 20)]
        public string ViewportSize { get; set; }

        /// <summary>
        /// Gets or sets the document encoding.
        /// </summary>
        [Parameter("de", DefaultValue = "UTF-8", MaxLength = 20)]
        public string DocumentEncoding { get; set; }

        /// <summary>
        /// Gets or sets the screen colors.
        /// </summary>
        [Parameter("sd", MaxLength = 20)]
        public string ScreenColors { get; set; }

        /// <summary>
        /// Gets or sets the user language.
        /// </summary>
        [Parameter("ul", MaxLength = 20)]
        public string UserLanguage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether java is enabled.
        /// </summary>
        [Parameter("je")]
        public bool? JavaEnabled { get; set; }

        /// <summary>
        /// Gets or sets the flash version.
        /// </summary>
        [Parameter("fl", MaxLength = 20)]
        public string FlashVersion { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public ISystemInfoParameters Clone()
        {
            return new SystemInfoParameters(this);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
