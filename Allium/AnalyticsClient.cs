// <copyright file="AnalyticsClient.cs" company="Kolky">
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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;
    using Interfaces;
    using Interfaces.Parameters;
    using Parameters;
    using Properties;
    using Validation;

    /// <summary>
    /// Client for Google Analytics.
    /// </summary>
    internal class AnalyticsClient : IAnalyticsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsClient"/> class.
        /// </summary>
        /// <param name="useHttps">useHttps</param>
        /// <param name="sendToDebugServer">sendToDebugServer</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])", Justification = "Not really useful here.")]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)", Justification = "Not really useful here.")]
        public AnalyticsClient(bool useHttps, bool sendToDebugServer)
        {
            var assemblyName = Assembly.GetAssembly(typeof(AnalyticsClient)).GetName();
            var appVersion = $"{assemblyName.Version.Major}.{assemblyName.Version.Minor}.{assemblyName.Version.Revision}";
            var platformVersion = $"{Environment.OSVersion.Version.Major}.{Environment.OSVersion.Version.Minor}.{Environment.OSVersion.Version.Revision}";
            this.UserAgent = $"Allium/{appVersion} ({Environment.OSVersion.Platform}; {platformVersion}; {Environment.OSVersion.VersionString})";
            this.Factory = new GoogleAnalyticsWebRequestFactory(useHttps, sendToDebugServer);
        }

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        public string UserAgent { get; private set; }

        /// <summary>
        /// Gets or sets the WebRequest creation factory.
        /// </summary>
        public IWebRequestCreate Factory { get; set; }

        /// <summary>
        /// Send parameters to Google Analytics.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Send(IGeneralParameters parameters)
        {
            Requires.NotNull(parameters, nameof(parameters));

            HttpWebResponse response;
            try
            {
                response = await this.ExecuteRequest(parameters.ConvertParameters());
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    return new AnalyticsResult(true);
                }
            }
            catch (Exception exception)
            {
                return new AnalyticsResult(exception);
            }

            return new AnalyticsResult(new AnalyticsException(string.Format(Resources.InvalidResponse, response.StatusCode)));
        }

        private async Task<HttpWebResponse> ExecuteRequest(IDictionary<string, string> parameters)
        {
            try
            {
                var data = string.Concat(parameters.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
                var request = this.Factory.Create(new Uri($"invalid://host?{data}")) as HttpWebRequest;
                request.UserAgent = this.UserAgent;
                if (parameters.ContainsKey("DocumentReferrer"))
                {
                    request.Headers.Add(HttpRequestHeader.Referer, parameters["ReferralUrl"]);
                }

                return await request.GetResponseAsync() as HttpWebResponse;
            }
            catch
            {
                return null;
            }
        }
    }
}