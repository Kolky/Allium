// <copyright file="PageViewHitParameters.cs" company="Kolky">
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
    /// Parameters for a PageView hit.
    /// </summary>
    internal class PageViewHitParameters : HitParameters, IPageViewParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="url">url</param>
        public PageViewHitParameters(IGeneralParameters copy, string url)
            : base(copy)
        {
            Requires.NotNullOrWhiteSpace(url, nameof(url));

            this.DocumentLocationUrl = url;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="hostName">hostName</param>
        /// <param name="path">path</param>
        public PageViewHitParameters(IGeneralParameters copy, string hostName, string path)
            : base(copy)
        {
            Requires.NotNullOrWhiteSpace(hostName, nameof(hostName));
            Requires.NotNullOrWhiteSpace(path, nameof(path));

            this.DocumentHostName = hostName;
            this.DocumentPath = path;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.PageView; }
        }

        /// <summary>
        /// Gets or sets the document location url.
        /// </summary>
        [Parameter("dl", NecessarySet = nameof(PageViewHitParameters))]
        public string DocumentLocationUrl { get; set; }

        /// <summary>
        /// Gets or sets the document host name.
        /// </summary>
        [Parameter("dh", NecessarySet = nameof(PageViewHitParameters), MutuallyExclusiveSet = nameof(PageViewHitParameters))]
        public string DocumentHostName { get; set; }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        [Parameter("dp", NecessarySet = nameof(PageViewHitParameters), MutuallyExclusiveSet = nameof(PageViewHitParameters))]
        public string DocumentPath { get; set; }
    }
}
