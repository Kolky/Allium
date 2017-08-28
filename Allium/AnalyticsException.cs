// <copyright file="AnalyticsException.cs" company="Kolky">
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
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception used in Allium.
    /// </summary>
    [Serializable]
    public sealed class AnalyticsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsException"/> class.
        /// </summary>
        public AnalyticsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        public AnalyticsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="innerException">innerException</param>
        public AnalyticsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsException"/> class.
        /// </summary>
        /// <param name="info">info</param>
        /// <param name="context">context</param>
        private AnalyticsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
