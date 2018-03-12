// <copyright file="TimingHitParameters.cs" company="Kolky">
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
    /// Parameters for a timing hit.
    /// </summary>
    internal class TimingHitParameters : CloneableHitParameters<ITimingParameters>, ITimingParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimingHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="category">category</param>
        /// <param name="name">name</param>
        public TimingHitParameters(IGeneralParameters copy, string category, string name)
            : base(copy)
        {
            Requires.NotNullOrWhiteSpace(category, nameof(category));
            Requires.NotNullOrWhiteSpace(name, nameof(name));

            this.UserTimingCategory = category;
            this.UserTimingVariableName = name;
        }

        private TimingHitParameters(TimingHitParameters copy)
            : base(copy)
        {
            this.UserTimingCategory = copy.UserTimingCategory;
            this.UserTimingVariableName = copy.UserTimingVariableName;
            this.UserTimingTime = copy.UserTimingTime;
            this.UserTimingLabel = copy.UserTimingLabel;
            this.PageLoadTime = copy.PageLoadTime;
            this.DnsTime = copy.DnsTime;
            this.PageDownloadTime = copy.PageDownloadTime;
            this.RedirectResponseTime = copy.RedirectResponseTime;
            this.TcpConnectTime = copy.TcpConnectTime;
            this.DomInteractiveTime = copy.DomInteractiveTime;
            this.ContentLoadTime = copy.ContentLoadTime;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.Timing; }
        }

        /// <summary>
        /// Gets or sets the user timing category.
        /// </summary>
        [Parameter("utc", Required = true, MaxLength = 150)]
        public string UserTimingCategory { get; set; }

        /// <summary>
        /// Gets or sets the user timing variable name.
        /// </summary>
        [Parameter("utv", Required = true, MaxLength = 500)]
        public string UserTimingVariableName { get; set; }

        /// <summary>
        /// Gets or sets the user timing time.
        /// </summary>
        [Parameter("utt", Required = true)]
        public int UserTimingTime { get; set; }

        /// <summary>
        /// Gets or sets the user timing label.
        /// </summary>
        [Parameter("utl", MaxLength = 500)]
        public string UserTimingLabel { get; set; }

        /// <summary>
        /// Gets or sets the page load time.
        /// </summary>
        [Parameter("ptl")]
        public int PageLoadTime { get; set; }

        /// <summary>
        /// Gets or sets the dns time.
        /// </summary>
        [Parameter("dns")]
        public int DnsTime { get; set; }

        /// <summary>
        /// Gets or sets the page download time.
        /// </summary>
        [Parameter("pdt")]
        public int PageDownloadTime { get; set; }

        /// <summary>
        /// Gets or sets the redirect response time.
        /// </summary>
        [Parameter("rrt")]
        public int RedirectResponseTime { get; set; }

        /// <summary>
        /// Gets or sets the tcp connect time.
        /// </summary>
        [Parameter("tcp")]
        public int TcpConnectTime { get; set; }

        /// <summary>
        /// Gets or sets the server response time.
        /// </summary>
        [Parameter("srt")]
        public int ServerResponseTime { get; set; }

        /// <summary>
        /// Gets or sets the dom interactive time.
        /// </summary>
        [Parameter("dit")]
        public int DomInteractiveTime { get; set; }

        /// <summary>
        /// Gets or sets the content load time.
        /// </summary>
        [Parameter("clt")]
        public int ContentLoadTime { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public override ITimingParameters Clone()
        {
            return new TimingHitParameters(this);
        }
    }
}
