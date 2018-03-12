// <copyright file="ICloneableGeneralParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Interfaces.Parameters
{
    /// <summary>
    /// Interface for cloneable version of general parameters.
    /// </summary>
    /// <typeparam name="T">Type of cloneable parameters.</typeparam>
    public interface ICloneableGeneralParameters<T> : IGeneralParameters, ICloneable<T>
    {
    }
}
