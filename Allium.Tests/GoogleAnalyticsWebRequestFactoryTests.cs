// <copyright file="GoogleAnalyticsWebRequestFactoryTests.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Allium;
    using NUnit.Framework;

    /// <summary>
    /// Fixture for <see cref="GoogleAnalyticsWebRequestFactory"/>.
    /// </summary>
    [TestFixture]
    public class GoogleAnalyticsWebRequestFactoryTests
    {
        /// <summary>
        /// Test for constructor.
        /// </summary>
        [Test]
        public void GoogleAnalyticsWebRequestFactoryTest()
        {
            this.TestFactory(new GoogleAnalyticsWebRequestFactory(true, true), true, true);
            this.TestFactory(new GoogleAnalyticsWebRequestFactory(true, false), true, false);
            this.TestFactory(new GoogleAnalyticsWebRequestFactory(false, true), false, true);
            this.TestFactory(new GoogleAnalyticsWebRequestFactory(false, false), false, false);
        }

        /// <summary>
        /// Test for method <see cref="GoogleAnalyticsWebRequestFactory.Create(System.Uri)"/>.
        /// </summary>
        [Test]
        public void CreateTest()
        {
            var factory = new GoogleAnalyticsWebRequestFactory(true, true);
            var request = factory.Create(new Uri("invalid://host?data=true"));
            Assert.NotNull(request);
            Assert.NotNull(request.RequestUri);
            Assert.AreEqual("https://ssl.google-analytics.com/debug/collect", request.RequestUri.ToString());
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)", Justification = "Not needed for uri's.")]
        private void TestFactory(GoogleAnalyticsWebRequestFactory factory, bool useHttps, bool sendToDebugServer)
        {
            Assert.NotNull(factory);
            Assert.AreEqual(sendToDebugServer, factory.SendToDebugServer);
            Assert.AreEqual(useHttps, factory.UseHttps);
            var baseUrl = useHttps ? "https://ssl" : "http://www";
            var testUrl = sendToDebugServer ? $"{baseUrl}.google-analytics.com/debug/collect" : $"{baseUrl}.google-analytics.com/collect";
            Assert.AreEqual(testUrl, factory.BeaconUrl.ToString());
        }
    }
}