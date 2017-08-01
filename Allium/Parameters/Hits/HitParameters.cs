// <copyright file="HitParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Parameters.Hits
{
    using Attributes;
    using Enums;
    using Interfaces.Parameters;

    /// <summary>
    /// Base for hit parameters.
    /// </summary>
    internal abstract class HitParameters : GeneralParameters, IHitParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HitParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected HitParameters(IGeneralParameters copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        [Parameter("t", Required = true)]
        public abstract HitType HitType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether a hit must be considered non-interactive.
        /// </summary>
        [Parameter("ni")]
        public bool? NonInteractionHit { get; set; }
    }
}
