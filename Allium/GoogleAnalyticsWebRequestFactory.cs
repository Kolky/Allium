// <copyright file="GoogleAnalyticsWebRequestFactory.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium
{
    using System;
    using System.Net;

    /// <summary>
    /// WebRequest factory for GoogleAnalytics.
    /// </summary>
    public class GoogleAnalyticsWebRequestFactory : IWebRequestCreate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleAnalyticsWebRequestFactory"/> class.
        /// </summary>
        /// <param name="useHttps">useHttps</param>
        /// <param name="sendToDebugServer">sendToDebugServer</param>
        public GoogleAnalyticsWebRequestFactory(bool useHttps, bool sendToDebugServer)
        {
            this.UseHttps = useHttps;
            this.SendToDebugServer = sendToDebugServer;
        }

        /// <summary>
        /// Gets a value indicating whether we are using a secure connection.
        /// </summary>
        public bool UseHttps { get; }

        /// <summary>
        /// Gets a value indicating whether we are in debug mode.
        /// </summary>
        public bool SendToDebugServer { get; }

        /// <summary>
        /// Gets the beacon url.
        /// </summary>
        public Uri BeaconUrl
        {
            get
            {
                return new Uri($"http{(this.UseHttps ? "s://ssl" : "://www")}.google-analytics.com/{(this.SendToDebugServer ? "debug/" : string.Empty)}collect");
            }
        }

        /// <summary>
        /// Create a WebRequest.
        /// </summary>
        /// <param name="uri">uri</param>
        /// <returns>The WebRequest that was created.</returns>
        public WebRequest Create(Uri uri)
        {
            return WebRequest.Create(new Uri(this.BeaconUrl, uri.Query));
        }
    }
}
