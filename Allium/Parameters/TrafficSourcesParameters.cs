// <copyright file="TrafficSourcesParameters.cs" company="Kolky">
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
    /// Parameters for traffic sources.
    /// </summary>
    internal class TrafficSourcesParameters : ITrafficSourcesParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficSourcesParameters"/> class.
        /// </summary>
        public TrafficSourcesParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficSourcesParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected TrafficSourcesParameters(TrafficSourcesParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.DocumentReferrer = copy.DocumentReferrer;
            this.CampaignName = copy.CampaignName;
            this.CampaignSource = copy.CampaignSource;
            this.CampaignMedium = copy.CampaignMedium;
            this.CampaignKeyword = copy.CampaignKeyword;
            this.CampaignContent = copy.CampaignContent;
            this.CampaignId = copy.CampaignId;
            this.GoogleAdWordsId = copy.GoogleAdWordsId;
            this.GoogleDisplayAdsId = copy.GoogleDisplayAdsId;
        }

        /// <summary>
        /// Gets or sets the document referrer.
        /// </summary>
        [Parameter("dr", MaxLength = 2048)]
        public string DocumentReferrer { get; set; }

        /// <summary>
        /// Gets or sets the campaign name.
        /// </summary>
        [Parameter("cn", MaxLength = 100)]
        public string CampaignName { get; set; }

        /// <summary>
        /// Gets or sets the campaign source.
        /// </summary>
        [Parameter("cs", MaxLength = 100)]
        public string CampaignSource { get; set; }

        /// <summary>
        /// Gets or sets the campaign medium.
        /// </summary>
        [Parameter("cm", MaxLength = 50)]
        public string CampaignMedium { get; set; }

        /// <summary>
        /// Gets or sets the campaign keyword.
        /// </summary>
        [Parameter("ck", MaxLength = 500)]
        public string CampaignKeyword { get; set; }

        /// <summary>
        /// Gets or sets the campaign content.
        /// </summary>
        [Parameter("cc", MaxLength = 500)]
        public string CampaignContent { get; set; }

        /// <summary>
        /// Gets or sets the campaign id.
        /// </summary>
        [Parameter("ci", MaxLength = 100)]
        public string CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the google ad-words id.
        /// </summary>
        [Parameter("gclid")]
        public string GoogleAdWordsId { get; set; }

        /// <summary>
        /// Gets or sets the google display-ads id.
        /// </summary>
        [Parameter("dclid")]
        public string GoogleDisplayAdsId { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public ITrafficSourcesParameters Clone()
        {
            return new TrafficSourcesParameters(this);
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
