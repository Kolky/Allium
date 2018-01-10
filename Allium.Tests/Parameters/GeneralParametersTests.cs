// <copyright file="GeneralParametersTests.cs" company="Kolky">
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
    using Allium.Parameters;
    using NUnit.Framework;

    /// <summary>
    /// Fixture for <see cref="GeneralParameters"/>.
    /// </summary>
    [TestFixture]
    public class GeneralParametersTests
    {
        /// <summary>
        /// Test for method <see cref="GeneralParametersExtensions.ConvertParameters"/>.
        /// </summary>
        [Test]
        public void MoreThan200CustomDimensionsTest()
        {
            var parameters = new GeneralParameters(AlliumConstants.TestTrackingId);
            for (int i = 1; i <= 200; ++i)
            {
                parameters.CustomDimensions.Add($"CustomDimension{i}");
            }

            // Add another record beyond 200
            parameters.CustomDimensions.Add("CustomDimensionXXX");

            AlliumAssert.Parameters(parameters);
            var dictionary = parameters.ConvertParameters();
            Assert.IsNotNull(dictionary);

            // Check if we have 200 dimensions
            for (int i = 1; i <= 200; ++i)
            {
                Assert.IsTrue(dictionary.ContainsKey($"cd{i}"));
                Assert.AreEqual($"CustomDimension{i}", dictionary[$"cd{i}"]);
            }

            // Check that no other dimensions exist
            Assert.IsFalse(dictionary.ContainsKey("cd201"));
        }
    }
}
