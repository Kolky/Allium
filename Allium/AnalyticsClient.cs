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
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;
    using Interfaces;
    using Interfaces.Parameters;
    using Parameters.Attributes;
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
        public AnalyticsClient(bool useHttps, bool sendToDebugServer)
        {
            this.IsSecure = useHttps;
            this.IsDebug = sendToDebugServer;

            var assemblyName = Assembly.GetAssembly(typeof(AnalyticsClient)).GetName();
            var appVersion = $"{assemblyName.Version.Major}.{assemblyName.Version.Minor}.{assemblyName.Version.Revision}";
            var platformVersion = $"{Environment.OSVersion.Version.Major}.{Environment.OSVersion.Version.Minor}.{Environment.OSVersion.Version.Revision}";
            this.UserAgent = $"Allium/{appVersion} ({Environment.OSVersion.Platform}; {platformVersion}; {Environment.OSVersion.VersionString})";
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are in debug mode.
        /// </summary>
        public bool IsDebug { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we are using a secure connection.
        /// </summary>
        public bool IsSecure { get; set; }

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        public string UserAgent { get; private set; }

        /// <summary>
        /// Gets the beacon url.
        /// </summary>
        public string BeaconUrl
        {
            get
            {
                return $"http{(this.IsSecure ? "s://ssl" : "://www")}.google-analytics.com/{(this.IsDebug ? "debug/" : string.Empty)}collect";
            }
        }

        /// <summary>
        /// Send parameters to Google Analytics.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Send(IGeneralParameters parameters)
        {
            Requires.NotNull(parameters, nameof(parameters));

            try
            {
                var response = await this.ExecuteRequest(this.ConvertParameters(parameters));
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    return new AnalyticsResult(true, null);
                }
            }
            catch (Exception exception)
            {
                return new AnalyticsResult(false, exception);
            }

            return new AnalyticsResult(false, null);
        }

        private IDictionary<string, string> ConvertParameters(object parameters)
        {
            var parsedParameters = new Dictionary<string, string>();

            foreach (var property in parameters.GetType().GetProperties(BindingFlags.Public))
            {
                // Try a regular parameter
                var paramAttrib = property.GetCustomAttribute<ParameterAttribute>(true);
                if (paramAttrib != null)
                {
                    var nullableType = Nullable.GetUnderlyingType(property.PropertyType);
                    if (property.PropertyType.IsEnum || (nullableType != null && nullableType.IsEnum))
                    {
                        var propValue = property.GetMethod.Invoke(parameters, null);
                        if (propValue != null)
                        {
                            var enumValue = paramAttrib.UseEnumValue
                                ? ((int)propValue).ToString(CultureInfo.InvariantCulture)
                                : propValue.ToString().ToLowerInvariant();
                            parsedParameters.Add(paramAttrib.ParameterName, enumValue);
                        }
                    }
                    else
                    {
                        var propValue = property.GetMethod.Invoke(parameters, null)?.ToString();
                        if (!string.IsNullOrWhiteSpace(propValue))
                        {
                            parsedParameters.Add(paramAttrib.ParameterName, propValue);
                        }
                    }
                }

                // Try a collection parameter
                var paramCollectionAttrib = property.GetCustomAttribute<ParameterCollectionAttribute>(true);
                if (paramCollectionAttrib != null)
                {
                    int counter = paramCollectionAttrib.StartIndex;
                    var propValue = property.GetMethod.Invoke(parameters, null) as IEnumerable<string>;
                    foreach (var item in propValue.Take(paramCollectionAttrib.MaxItems))
                    {
                        parsedParameters.Add(
                            paramCollectionAttrib.ParameterName + counter.ToString(CultureInfo.InvariantCulture),
                            item.ToString());
                    }
                }

                // Try the object as a new parameter object
                var containsParamsAttrib = property.GetCustomAttribute<ContainsParametersAttribute>(true);
                if (containsParamsAttrib != null)
                {
                    foreach (var record in this.ConvertParameters(property.GetValue(parameters)))
                    {
                        parsedParameters.Add(record.Key, record.Value);
                    }
                }
            }

            return parsedParameters;
        }

        private async Task<HttpWebResponse> ExecuteRequest(IDictionary<string, string> parameters)
        {
            try
            {
                var data = string.Concat(parameters.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
                var request = WebRequest.CreateHttp($"{this.BeaconUrl}?{data}");
                request.Headers.Add(HttpRequestHeader.UserAgent, this.UserAgent);
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