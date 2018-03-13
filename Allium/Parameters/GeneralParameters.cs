// <copyright file="GeneralParameters.cs" company="Kolky">
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
    using System.Collections.Generic;
    using Attributes;
    using Interfaces.Parameters;
    using Validation;

    /// <summary>
    /// Contains all general parameters.
    /// </summary>
    internal class GeneralParameters : IGeneralParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralParameters"/> class.
        /// </summary>
        /// <param name="trackingId">trackingId</param>
        public GeneralParameters(string trackingId)
        {
            Requires.NotNullOrWhiteSpace(trackingId, nameof(trackingId));

            this.TrackingId = trackingId;
            this.User = new UserParameters(Guid.NewGuid());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralParameters"/> class.
        /// </summary>
        /// <param name="trackingId">trackingId</param>
        /// <param name="clientId">clientId</param>
        public GeneralParameters(string trackingId, Guid clientId)
        {
            Requires.NotNullOrWhiteSpace(trackingId, nameof(trackingId));

            this.TrackingId = trackingId;
            this.User = new UserParameters(clientId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralParameters"/> class.
        /// </summary>
        /// <param name="trackingId">trackingId</param>
        /// <param name="userId">userId</param>
        public GeneralParameters(string trackingId, string userId)
        {
            Requires.NotNullOrWhiteSpace(trackingId, nameof(trackingId));

            this.TrackingId = trackingId;
            this.User = new UserParameters(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralParameters"/> class.
        /// Copy constructor.
        /// </summary>
        /// <param name="copy">copy</param>
        public GeneralParameters(IGeneralParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.TrackingId = copy.TrackingId;
            this.AnonymizeIP = copy.AnonymizeIP;
            this.DataSource = copy.DataSource;
            this.QueueTime = copy.QueueTime;
            this.CacheBuster = copy.CacheBuster;

            this.User = copy.User.Clone();
            this.Session = copy.Session.Clone();
            this.TrafficSources = copy.TrafficSources.Clone();
            this.SystemInfo = copy.SystemInfo.Clone();
            this.ContentInformation = copy.ContentInformation.Clone();
            this.App = copy.App.Clone();
            this.ContentExperiments = copy.ContentExperiments.Clone();
            this.EnhancedEcommerce = copy.EnhancedEcommerce.Clone();

            this.CustomDimensions = new List<string>(copy.CustomDimensions);
            this.CustomMetrics = new List<string>(copy.CustomMetrics);
        }

        /// <summary>
        /// Gets the protocol version.
        /// </summary>
        [Parameter("v", Required = true)]
        public string ProtocolVersion
        {
            get { return "1"; }
        }

        /// <summary>
        /// Gets or sets the tracking code.
        /// </summary>
        [Parameter("tid", Required = true)]
        public string TrackingId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the IP address of the sender will be anonymized.
        /// </summary>
        [Parameter("aip")]
        public bool? AnonymizeIP { get; set; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        [Parameter("ds")]
        public string DataSource { get; set; }

        /// <summary>
        /// Gets or sets a value that represents the time the hit was in the queue. (in milliseconds)
        /// </summary>
        [Parameter("qt")]
        public int? QueueTime { get; set; }

        /// <summary>
        /// Gets or sets a random number in GET requests to ensure browsers and proxies don't cache hits.
        /// </summary>
        [Parameter("z")]
        public string CacheBuster { get; set; }

        /// <summary>
        /// Gets the user parameters.
        /// </summary>
        [ContainsParameters]
        public IUserParameters User { get; private set; }

        /// <summary>
        /// Gets the session parameters.
        /// </summary>
        [ContainsParameters]
        public ISessionParameters Session { get; } = new SessionParameters();

        /// <summary>
        /// Gets the traffic sources parameters.
        /// </summary>
        [ContainsParameters]
        public ITrafficSourcesParameters TrafficSources { get; } = new TrafficSourcesParameters();

        /// <summary>
        /// Gets the system info parameters.
        /// </summary>
        [ContainsParameters]
        public ISystemInfoParameters SystemInfo { get; } = new SystemInfoParameters();

        /// <summary>
        /// Gets the content information parameters.
        /// </summary>
        [ContainsParameters]
        public IContentInformationParameters ContentInformation { get; } = new ContentInformationParameters();

        /// <summary>
        /// Gets the app tracking parameters.
        /// </summary>
        [ContainsParameters]
        public IAppTrackingParameters App { get; } = new AppTrackingParameters();

        /// <summary>
        /// Gets the content experiments parameters.
        /// </summary>
        [ContainsParameters]
        public IContentExperimentsParameters ContentExperiments { get; } = new ContentExperimentsParameters();

        /// <summary>
        /// Gets the enhanced E-Commerce parameters.
        /// </summary>
        [ContainsParameters]
        public IEnhancedEcommerceParameters EnhancedEcommerce { get; } = new EnhancedEcommerceParameters();

        /// <summary>
        /// Gets a list of custom dimensions.
        /// </summary>
        [ParameterCollection("cd", StartIndex = 1, MaxItems = 200)]
        public IList<string> CustomDimensions { get; } = new List<string>();

        /// <summary>
        /// Gets a list of custom metrics.
        /// </summary>
        [ParameterCollection("cm", StartIndex = 1, MaxItems = 200)]
        public IList<string> CustomMetrics { get; } = new List<string>();
    }
}
