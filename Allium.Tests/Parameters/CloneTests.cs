// <copyright file="CloneTests.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Tests.Parameters
{
    using System;
    using Allium.Parameters;
    using NUnit.Framework;

    /// <summary>
    /// Fixture for all parameter classes.
    /// </summary>
    [TestFixture]
    public class CloneTests
    {
        /// <summary>
        /// Test for <see cref="AppTrackingParameters.Clone"/>.
        /// </summary>
        [Test]
        public void AppTrackingParametersCloneTest()
        {
            ICloneable parameters = new AppTrackingParameters();
            Assert.NotNull(parameters);
            Assert.AreNotSame(parameters, parameters.Clone());
        }

        /// <summary>
        /// Test for <see cref="ContentExperimentsParameters.Clone"/>.
        /// </summary>
        [Test]
        public void ContentExperimentsParametersCloneTest()
        {
            ICloneable parameters = new ContentExperimentsParameters();
            Assert.NotNull(parameters);
            Assert.AreNotSame(parameters, parameters.Clone());
        }

        /// <summary>
        /// Test for <see cref="ContentInformationParameters.Clone"/>.
        /// </summary>
        [Test]
        public void ContentInformationParametersCloneTest()
        {
            ICloneable parameters = new ContentInformationParameters();
            Assert.NotNull(parameters);
            Assert.AreNotSame(parameters, parameters.Clone());
        }

        /// <summary>
        /// Test for <see cref="GeneralParameters.Clone"/>.
        /// </summary>
        [Test]
        public void GeneralParametersCloneTest()
        {
            ICloneable parameters = new GeneralParameters("TrackingId");
            Assert.NotNull(parameters);
            Assert.AreNotSame(parameters, parameters.Clone());
        }

        /// <summary>
        /// Test for <see cref="SessionParameters.Clone"/>;
        /// </summary>
        [Test]
        public void SessionParametersCloneTest()
        {
            ICloneable parameters = new SessionParameters();
            Assert.NotNull(parameters);
            Assert.AreNotSame(parameters, parameters.Clone());
        }

        /// <summary>
        /// Test for <see cref="SystemInfoParameters.Clone"/>.
        /// </summary>
        [Test]
        public void SystemInfoParametersCloneTest()
        {
            ICloneable parameters = new SystemInfoParameters();
            Assert.NotNull(parameters);
            Assert.AreNotSame(parameters, parameters.Clone());
        }

        /// <summary>
        /// Test for <see cref="TrafficSourcesParameters.Clone"/>.
        /// </summary>
        [Test]
        public void TrafficSourcesParametersCloneTest()
        {
            ICloneable parameters = new TrafficSourcesParameters();
            Assert.NotNull(parameters);
            Assert.AreNotSame(parameters, parameters.Clone());
        }

        /// <summary>
        /// Test for <see cref="UserParameters.Clone"/>.
        /// </summary>
        [Test]
        public void UserParametersCloneTest()
        {
            ICloneable parameters = new UserParameters();
            Assert.NotNull(parameters);
            Assert.AreNotSame(parameters, parameters.Clone());
        }
    }
}
