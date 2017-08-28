// <copyright file="IGeneralParameters.cs" company="Kolky">
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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface with general parameters.
    /// <a href="https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#general"/>
    /// </summary>
    public interface IGeneralParameters : ICloneable<IGeneralParameters>
    {
        /// <summary>
        /// Gets the protocol version.
        /// </summary>
        string ProtocolVersion { get; }

        /// <summary>
        /// Gets or sets the tracking code.
        /// </summary>
        string TrackingId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the IP address of the sender will be anonymized.
        /// </summary>
        bool? AnonymizeIP { get; set; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        string DataSource { get; set; }

        /// <summary>
        /// Gets or sets a value that represents the time the hit was in the queue. (in milliseconds)
        /// </summary>
        int? QueueTime { get; set; }

        /// <summary>
        /// Gets or sets a random number in GET requests to ensure browsers and proxies don't cache hits.
        /// </summary>
        string CacheBuster { get; set; }

        /// <summary>
        /// Gets the user parameters.
        /// </summary>
        IUserParameters User { get; }

        /// <summary>
        /// Gets the session parameters.
        /// </summary>
        ISessionParameters Session { get; }

        /// <summary>
        /// Gets the traffic sources parameters.
        /// </summary>
        ITrafficSourcesParameters TrafficSources { get; }

        /// <summary>
        /// Gets the system info parameters.
        /// </summary>
        ISystemInfoParameters SystemInfo { get; }

        /// <summary>
        /// Gets the content information parameters.
        /// </summary>
        IContentInformationParameters ContentInformation { get; }

        /// <summary>
        /// Gets the app parameters.
        /// </summary>
        IAppTrackingParameters App { get; }

        /// <summary>
        /// Gets the content experiments.
        /// </summary>
        IContentExperimentsParameters ContentExperiments { get; }

        /// <summary>
        /// Gets a list of custom dimensions.
        /// </summary>
        IList<string> CustomDimensions { get; }

        /// <summary>
        /// Gets a list of custom metrics.
        /// </summary>
        IList<string> CustomMetrics { get; }
    }
}
