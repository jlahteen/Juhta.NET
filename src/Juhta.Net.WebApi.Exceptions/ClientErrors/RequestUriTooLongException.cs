﻿
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.ClientErrors
{
    /// <summary>
    /// Defines an exception class for the HTTP error Request URI Too Long.
    /// </summary>
    public class RequestUriTooLongException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public RequestUriTooLongException() : base(HttpStatusCode.RequestUriTooLong)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        public RequestUriTooLongException(Enum errorCode) : base(HttpStatusCode.RequestUriTooLong, null, errorCode.ToString())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public RequestUriTooLongException(string message) : base(HttpStatusCode.RequestUriTooLong, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public RequestUriTooLongException(Enum errorCode, string message) : base(HttpStatusCode.RequestUriTooLong, message, errorCode.ToString())
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientError">Specifies a client error based on which to create the instance.</param>
        internal RequestUriTooLongException(ClientError clientError) : base(clientError)
        {}

        #endregion
    }
}
