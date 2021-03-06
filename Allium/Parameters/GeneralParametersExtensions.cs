﻿// <copyright file="GeneralParametersExtensions.cs" company="Kolky">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Interfaces.Parameters;

    /// <summary>
    /// Extensions for <see cref="IGeneralParameters"/>.
    /// </summary>
    internal static class GeneralParametersExtensions
    {
        /// <summary>
        /// Convert parameters to a dictionary with key values based on it's attributes.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns>Dictionary</returns>
        public static IDictionary<string, string> ConvertParameters(this IGeneralParameters parameters)
        {
            return ConvertParameters((object)parameters);
        }

        private static IDictionary<string, string> ConvertParameters(object parameters)
        {
            var parsedParameters = new Dictionary<string, string>();

            foreach (var property in parameters.GetType().GetProperties())
            {
                // Try a regular parameter
                var paramAttrib = property.GetCustomAttribute<ParameterAttribute>(true);
                if (paramAttrib != null)
                {
                    ConvertRegularParameter(parameters, property, paramAttrib, parsedParameters);
                }

                // Try a collection parameter
                var paramCollectionAttrib = property.GetCustomAttribute<ParameterCollectionAttribute>(true);
                if (paramCollectionAttrib != null)
                {
                    ConvertCollectionParameter(parameters, property, paramCollectionAttrib, parsedParameters);
                }

                // Try the object as a new parameter object
                var containsParamsAttrib = property.GetCustomAttribute<ContainsParametersAttribute>(true);
                if (containsParamsAttrib != null)
                {
                    foreach (var record in ConvertParameters(property.GetValue(parameters)))
                    {
                        parsedParameters.Add(record.Key, record.Value);
                    }
                }
            }

            return parsedParameters;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "The API requires lower case.")]
        private static void ConvertRegularParameter(
            object parameters,
            PropertyInfo property,
            ParameterAttribute paramAttrib,
            IDictionary<string, string> parsedParameters)
        {
            var nullableType = Nullable.GetUnderlyingType(property.PropertyType);
            if (property.PropertyType.IsEnum || (nullableType != null && nullableType.IsEnum))
            {
                var propValue = property.GetMethod.Invoke(parameters, null);
                if (propValue != null)
                {
                    var enumValue = paramAttrib.UseEnumValue
                        ? ((int)propValue).ToString(CultureInfo.InvariantCulture)
                        : propValue.ToString().ToLowerInvariant();
                    parsedParameters.Add(paramAttrib.ParameterName, enumValue);
                }
            }
            else
            {
                var propValue = property.GetMethod.Invoke(parameters, null);
                var propValueString = propValue?.ToString();
                object propDefaultValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null;
                if ((!string.IsNullOrWhiteSpace(propValueString) && !property.PropertyType.IsValueType) || propValueString != propDefaultValue?.ToString())
                {
                    parsedParameters.Add(paramAttrib.ParameterName, propValueString);
                }
            }
        }

        private static void ConvertCollectionParameter(
            object parameters,
            PropertyInfo property,
            ParameterCollectionAttribute paramCollectionAttrib,
            IDictionary<string, string> parsedParameters)
        {
            int counter = paramCollectionAttrib.StartIndex;
            var propValue = property.GetMethod.Invoke(parameters, null) as IEnumerable<string>;
            foreach (var item in propValue.Take(paramCollectionAttrib.MaxItems))
            {
                parsedParameters.Add(
                    paramCollectionAttrib.ParameterName + counter.ToString(CultureInfo.InvariantCulture),
                    item.ToString());
                counter++;
            }
        }
    }
}
