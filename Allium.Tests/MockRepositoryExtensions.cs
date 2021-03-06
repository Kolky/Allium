﻿// <copyright file="MockRepositoryExtensions.cs" company="Kolky">
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
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Rhino.Mocks;
    using Validation;

    /// <summary>
    /// Extensions for <see cref="MockRepository"/>.
    /// </summary>
    public static class MockRepositoryExtensions
    {
        /// <summary>
        /// Expect a WebRequest with StatusCode.
        /// </summary>
        /// <param name="repository">repository</param>
        /// <param name="factory">factory</param>
        /// <param name="statusCode">statucCode</param>
        /// <returns>repository (to chain results)</returns>
        public static MockRepository ExpectWebRequest(this MockRepository repository, IWebRequestCreate factory, HttpStatusCode statusCode)
        {
            Requires.NotNull(repository, nameof(repository));

            var requestHeaders = repository.PartialMock<WebHeaderCollection>();
            var stream = repository.PartialMock<MemoryStream>();
            var request = repository.Stub<HttpWebRequest>();
            var response = repository.PartialMock<HttpWebResponse>();

            // TODO: Expect parameters in URI!
            factory.Expect(x => x.Create(Arg<Uri>.Is.Anything)).Return(request).Repeat.Once();
            request.Headers = requestHeaders;
            request.Expect(x => x.GetRequestStreamAsync()).Return(Task.FromResult<Stream>(stream)).Repeat.Any();
            request.Expect(x => x.GetResponseAsync()).Return(Task.FromResult<WebResponse>(response)).Repeat.Once();
            response.Expect(x => x.StatusCode).Return(statusCode).Repeat.Any();

            return repository;
        }
    }
}
