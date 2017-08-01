// <copyright file="AppTrackingParameters.cs" company="Kolky">
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
    /// Parameters for app tracking.
    /// </summary>
    internal class AppTrackingParameters : IAppTrackingParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppTrackingParameters"/> class.
        /// </summary>
        public AppTrackingParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppTrackingParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected AppTrackingParameters(AppTrackingParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.ApplicationName = copy.ApplicationName;
            this.ApplicationId = copy.ApplicationId;
            this.ApplicationVersion = copy.ApplicationVersion;
            this.ApplicationInstallerId = copy.ApplicationInstallerId;
        }

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        [Parameter("an", Required = true, MaxLength = 100)]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets te application identifier.
        /// </summary>
        [Parameter("aid", MaxLength = 150)]
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        [Parameter("av", MaxLength = 100)]
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// Gets or sets the application installer identifier.
        /// </summary>
        [Parameter("aiid", MaxLength = 150)]
        public string ApplicationInstallerId { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public IAppTrackingParameters Clone()
        {
            return new AppTrackingParameters(this);
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
