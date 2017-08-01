// <copyright file="ContentExperimentsParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Parameters
{
    using System;
    using Attributes;
    using Interfaces.Parameters;
    using Validation;

    /// <summary>
    /// Parameters for content experiments.
    /// </summary>
    internal class ContentExperimentsParameters : IContentExperimentsParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentExperimentsParameters"/> class.
        /// </summary>
        public ContentExperimentsParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentExperimentsParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected ContentExperimentsParameters(ContentExperimentsParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.ExperimentId = copy.ExperimentId;
            this.ExperimentVariant = copy.ExperimentVariant;
        }

        /// <summary>
        /// Gets or sets the experiment id.
        /// </summary>
        [Parameter("xid", MaxLength = 40)]
        public string ExperimentId { get; set; }

        /// <summary>
        /// Gets or sets the experiment variant.
        /// </summary>
        [Parameter("xvar")]
        public string ExperimentVariant { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public IContentExperimentsParameters Clone()
        {
            return new ContentExperimentsParameters(this);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
