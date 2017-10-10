// <copyright file="SessionParametersTests.cs" company="Kolky">
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
    using System.Net;
    using Allium.Parameters;
    using NUnit.Framework;

    /// <summary>
    /// Fixture for <see cref="SessionParameters"/>.
    /// </summary>
    [TestFixture]
    public class SessionParametersTests
    {
        /// <summary>
        /// Test for constructor <see cref="SessionParameters(SessionParameters)"/>.
        /// </summary>
        [Test]
        public void SessionParametersTest()
        {
            var parameters = new SessionParameters();
            parameters.IPOverride = IPAddress.Parse("127.0.0.1");
            Assert.NotNull(parameters);
            Assert.AreEqual("127.0.0.1", parameters.IPOverride.ToString());

            var clone = parameters.Clone();
            Assert.AreNotSame(parameters, clone);
            Assert.AreEqual("127.0.0.1", clone.IPOverride.ToString());
        }
    }
}
