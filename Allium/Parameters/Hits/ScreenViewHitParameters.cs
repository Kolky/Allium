// <copyright file="ScreenViewHitParameters.cs" company="Kolky">
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
    using Interfaces.Parameters.Hits;
    using Validation;

    /// <summary>
    /// Parameters for a ScreenView hit.
    /// </summary>
    internal class ScreenViewHitParameters : HitParameters, IScreenViewParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenViewHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="screen">screen</param>
        public ScreenViewHitParameters(IGeneralParameters copy, string screen)
            : base(copy)
        {
            Requires.NotNullOrWhiteSpace(screen, nameof(screen));

            this.ScreenName = screen;
        }

        private ScreenViewHitParameters(ScreenViewHitParameters copy)
            : base(copy)
        {
            this.ScreenName = copy.ScreenName;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.ScreenView; }
        }

        /// <summary>
        /// Gets or sets the screen name.
        /// </summary>
        [Parameter("cd", Required = true, MaxLength = 2048)]
        public string ScreenName { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public override IGeneralParameters Clone()
        {
            return new ScreenViewHitParameters(this);
        }
    }
}
