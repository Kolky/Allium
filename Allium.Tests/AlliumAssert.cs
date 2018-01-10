// <copyright file="AlliumAssert.cs" company="Kolky">
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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Allium.Interfaces;
    using Allium.Interfaces.Parameters;
    using NUnit.Framework;
    using Validation;

    /// <summary>
    /// Complex Asserts specific to Allium.
    /// </summary>
    public static class AlliumAssert
    {
        /// <summary>
        /// Assert a async task to be successfull.
        /// </summary>
        /// <param name="task">task</param>
        public static void Success(Task<IAnalyticsResult> task)
        {
            Requires.NotNull(task, nameof(task));
            task.Wait();

            Assert.NotNull(task.Result);
            Assert.IsTrue(task.Result.Success);
            Assert.IsNull(task.Result.Exception);
        }

        /// <summary>
        /// Assert a async task to have failed.
        /// </summary>
        /// <param name="task">task</param>
        /// <param name="message">message</param>
        public static void Failed(Task<IAnalyticsResult> task, string message)
        {
            Requires.NotNull(task, nameof(task));
            task.Wait();

            Assert.NotNull(task.Result);
            Assert.IsFalse(task.Result.Success);
            Assert.IsInstanceOf<AnalyticsException>(task.Result.Exception);
            Assert.AreEqual(message, task.Result.Exception.Message);
        }

        /// <summary>
        /// Assert default parameters.
        /// </summary>
        /// <param name="generalParameters">parameters</param>
        public static void Parameters(IGeneralParameters generalParameters)
        {
            Requires.NotNull(generalParameters, nameof(generalParameters));
            Assert.AreEqual(AlliumConstants.TestTrackingId, generalParameters.TrackingId);
            Assert.AreEqual("1", generalParameters.ProtocolVersion);
        }
    }
}
