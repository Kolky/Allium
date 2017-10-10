// <copyright file="ParameterCollectionAttribute.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Parameters.Attributes
{
    using System;
    using Validation;

    /// <summary>
    /// Attribute that describes a parameter with a collection of the tracker.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal sealed class ParameterCollectionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterCollectionAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">parameterName</param>
        public ParameterCollectionAttribute(string parameterName)
        {
            Requires.NotNullOrWhiteSpace(parameterName, nameof(parameterName));

            this.ParameterName = parameterName;
        }

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// Gets or sets the index to start counting at.
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets the maximum of items in the collection.
        /// </summary>
        public int MaxItems { get; set; }
    }
}
