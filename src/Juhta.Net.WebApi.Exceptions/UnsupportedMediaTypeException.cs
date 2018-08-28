﻿
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines an exception class for the HTTP error Unsupported Media Type.
    /// </summary>
    public class UnsupportedMediaTypeException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public UnsupportedMediaTypeException() : base(HttpStatusCode.UnsupportedMediaType)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public UnsupportedMediaTypeException(string message) : base(HttpStatusCode.UnsupportedMediaType, message)
        {}

        #endregion
    }
}
