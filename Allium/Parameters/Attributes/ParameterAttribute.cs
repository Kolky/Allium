// <copyright file="ParameterAttribute.cs" company="Kolky">
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
    /// Attribute that describes a parameter of the tracker.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal sealed class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">parameterName</param>
        public ParameterAttribute(string parameterName)
        {
            Requires.NotNullOrWhiteSpace(parameterName, nameof(parameterName));

            this.ParameterName = parameterName;
        }

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parameter is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the default value of the parameter.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the max length of the parameter value. (in bytes)
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the mutually exclusive set.
        /// </summary>
        public string MutuallyExclusiveSet { get; set; }

        /// <summary>
        /// Gets or sets the set of wich one is necessary.
        /// </summary>
        public string NecessarySet { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we want the underlying value, not a string.
        /// </summary>
        public bool UseEnumValue { get; set; }
    }
}
