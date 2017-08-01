// <copyright file="ContentInformationParameters.cs" company="Kolky">
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
    using System.Collections.Generic;
    using Attributes;
    using Interfaces.Parameters;
    using Validation;

    /// <summary>
    /// Parameters for content information.
    /// </summary>
    internal class ContentInformationParameters : IContentInformationParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentInformationParameters"/> class.
        /// </summary>
        public ContentInformationParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentInformationParameters"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected ContentInformationParameters(ContentInformationParameters copy)
        {
            Requires.NotNull(copy, nameof(copy));

            this.DocumentTitle = copy.DocumentTitle;
            this.ContentGroups = new List<string>(this.ContentGroups);
            this.LinkId = copy.LinkId;
        }

        /// <summary>
        /// Gets or sets the document title.
        /// </summary>
        [Parameter("dt", MaxLength = 1500)]
        public string DocumentTitle { get; set; }

        /// <summary>
        /// Gets the content groups.
        /// </summary>
        [ParameterCollection("cg", StartIndex = 1, MaxItems = 5)]
        public IList<string> ContentGroups { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the link id.
        /// </summary>
        [Parameter("linkid")]
        public string LinkId { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public IContentInformationParameters Clone()
        {
            return new ContentInformationParameters(this);
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
