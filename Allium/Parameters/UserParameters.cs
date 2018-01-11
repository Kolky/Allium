// <copyright file="UserParameters.cs" company="Kolky">
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
    /// Parameters for the user.
    /// </summary>
    internal class UserParameters : IUserParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserParameters"/> class.
        /// </summary>
        /// <param name="clientId">clientId</param>
        public UserParameters(Guid clientId)
        {
            this.ClientId = clientId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserParameters"/> class.
        /// </summary>
        /// <param name="userId">userId</param>
        public UserParameters(string userId)
        {
            this.UserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected UserParameters(UserParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.ClientId = copy.ClientId;
            this.UserId = copy.UserId;
        }

        /// <summary>
        /// Gets or sets the anonymous identification of a particular user, device, or browser instance.
        /// </summary>
        [Parameter("cid", NecessarySet = nameof(IUserParameters))]
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Parameter("uid", NecessarySet = nameof(IUserParameters))]
        public string UserId { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public IUserParameters Clone()
        {
            return new UserParameters(this);
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
