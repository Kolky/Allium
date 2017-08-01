// <copyright file="ITrafficSourcesParameters.cs" company="Kolky">
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
    /// Interface with the traffic sources parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#trafficsources"/>
    /// </summary>
    public interface ITrafficSourcesParameters : ICloneable<ITrafficSourcesParameters>
    {
        /// <summary>
        /// Gets or sets the document referrer.
        /// </summary>
        string DocumentReferrer { get; set; }

        /// <summary>
        /// Gets or sets the campaign name.
        /// </summary>
        string CampaignName { get; set; }

        /// <summary>
        /// Gets or sets the campaign source.
        /// </summary>
        string CampaignSource { get; set; }

        /// <summary>
        /// Gets or sets the campaign medium.
        /// </summary>
        string CampaignMedium { get; set; }

        /// <summary>
        /// Gets or sets the campaign keyword.
        /// </summary>
        string CampaignKeyword { get; set; }

        /// <summary>
        /// Gets or sets the campaign content.
        /// </summary>
        string CampaignContent { get; set; }

        /// <summary>
        /// Gets or sets the campaign id.
        /// </summary>
        string CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the google ad-words id.
        /// </summary>
        string GoogleAdWordsId { get; set; }

        /// <summary>
        /// Gets or sets the google display-ads id.
        /// </summary>
        string GoogleDisplayAdsId { get; set; }
    }
}
