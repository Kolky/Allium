// <copyright file="ExceptionHitParameters.cs" company="Kolky">
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
    using System;
    using Attributes;
    using Enums;
    using Interfaces.Parameters;
    using Interfaces.Parameters.Hits;

    /// <summary>
    /// Parameters for an exception hit.
    /// </summary>
    internal class ExceptionHitParameters : HitParameters, IExceptionParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="exception">exception</param>
        /// <param name="wasFatal">wasFatal</param>
        public ExceptionHitParameters(IGeneralParameters copy, Exception exception, bool? wasFatal)
            : base(copy)
        {
            this.ExceptionDescription = exception?.Message;
            this.IsExceptionFatal = wasFatal;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.Exception; }
        }

        /// <summary>
        /// Gets or sets the exception description.
        /// </summary>
        [Parameter("exd", MaxLength = 150)]
        public string ExceptionDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the exception was fatal.
        /// </summary>
        [Parameter("exf", DefaultValue = true)]
        public bool? IsExceptionFatal { get; set; }
    }
}
