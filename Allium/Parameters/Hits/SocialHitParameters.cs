// <copyright file="SocialHitParameters.cs" company="Kolky">
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
    /// Parameters for a social hit.
    /// </summary>
    internal class SocialHitParameters : HitParameters, ISocialParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="network">network</param>
        /// <param name="action">action</param>
        /// <param name="target">target</param>
        public SocialHitParameters(IGeneralParameters copy, string network, string action, string target)
            : base(copy)
        {
            Requires.NotNullOrWhiteSpace(network, nameof(network));
            Requires.NotNullOrWhiteSpace(action, nameof(action));
            Requires.NotNullOrWhiteSpace(target, nameof(target));

            this.SocialNetwork = network;
            this.SocialAction = action;
            this.SocialActionTarget = target;
        }

        private SocialHitParameters(SocialHitParameters copy)
            : base(copy)
        {
            this.SocialNetwork = copy.SocialNetwork;
            this.SocialAction = copy.SocialAction;
            this.SocialActionTarget = copy.SocialActionTarget;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.Social; }
        }

        /// <summary>
        /// Gets or sets the social network.
        /// </summary>
        [Parameter("sn", Required = true, MaxLength = 50)]
        public string SocialNetwork { get; set; }

        /// <summary>
        /// Gets or sets the social action.
        /// </summary>
        [Parameter("sa", Required = true, MaxLength = 50)]
        public string SocialAction { get; set; }

        /// <summary>
        /// Gets or sets the social action target. Typically a url.
        /// </summary>
        [Parameter("st", Required = true, MaxLength = 2048)]
        public string SocialActionTarget { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public override IGeneralParameters Clone()
        {
            return new SocialHitParameters(this);
        }
    }
}
