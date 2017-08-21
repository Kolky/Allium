// <copyright file="SessionParameters.cs" company="Kolky">
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
    using System.Net;
    using Attributes;
    using Enums;
    using Interfaces.Parameters;
    using Validation;

    /// <summary>
    /// Parameters for the session.
    /// </summary>
    internal class SessionParameters : ISessionParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionParameters"/> class.
        /// </summary>
        public SessionParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected SessionParameters(SessionParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.SessionControl = copy.SessionControl;
            this.IPOverride = copy.IPOverride != null ? new IPAddress(copy.IPOverride.GetAddressBytes()) : null;
            this.UserAgentOverride = copy.UserAgentOverride;
            this.GeographicalOverride = copy.GeographicalOverride;
        }

        /// <summary>
        /// Gets or sets the session control.
        /// </summary>
        [Parameter("sc", UseEnumValue = true)]
        public SessionControl? SessionControl { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the user.
        /// </summary>
        [Parameter("uip")]
        public IPAddress IPOverride { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        [Parameter("ua")]
        public string UserAgentOverride { get; set; }

        /// <summary>
        /// Gets or sets the geographical location of the user.
        /// <a href="http://developers.google.com/analytics/devguides/collection/protocol/v1/geoid"/>
        /// </summary>
        [Parameter("geoid")]
        public string GeographicalOverride { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public ISessionParameters Clone()
        {
            return new SessionParameters(this);
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
