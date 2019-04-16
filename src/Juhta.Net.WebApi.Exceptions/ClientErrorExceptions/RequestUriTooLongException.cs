﻿
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.ClientErrorExceptions
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
        public RequestUriTooLongException() : base(HttpStatusCode.RequestUriTooLong, null, null, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientError">Specifies a client error.</param>
        public RequestUriTooLongException(ClientError clientError) : base(HttpStatusCode.RequestUriTooLong, clientError)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        public RequestUriTooLongException(Enum errorCode) : base(HttpStatusCode.RequestUriTooLong, null, errorCode.ToString(), null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrors">Specifies a collection of client errors.</param>
        public RequestUriTooLongException(IEnumerable<ClientError> clientErrors) : base(HttpStatusCode.RequestUriTooLong, clientErrors)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public RequestUriTooLongException(string message) : base(HttpStatusCode.RequestUriTooLong, message, null, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        public RequestUriTooLongException(Enum errorCode, Enum field) : base(HttpStatusCode.RequestUriTooLong, null, errorCode.ToString(), field.ToString(), null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public RequestUriTooLongException(Enum errorCode, string message) : base(HttpStatusCode.RequestUriTooLong, message, errorCode.ToString(), null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="helpUrl">Specifies a URL that provides extra information about the error.</param>
        public RequestUriTooLongException(string message, string helpUrl) : base(HttpStatusCode.RequestUriTooLong, message, null, null, helpUrl)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        /// <param name="message">Specifies an error message.</param>
        public RequestUriTooLongException(Enum errorCode, Enum field, string message) : base(HttpStatusCode.RequestUriTooLong, message, errorCode.ToString(), field.ToString(), null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        /// <param name="message">Specifies an error message.</param>
        public RequestUriTooLongException(Enum errorCode, string field, string message) : base(HttpStatusCode.RequestUriTooLong, message, errorCode.ToString(), field, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="helpUrl">Specifies a URL that provides extra information about the error.</param>
        public RequestUriTooLongException(Enum errorCode, Enum field, string message, string helpUrl) : base(HttpStatusCode.RequestUriTooLong, message, errorCode.ToString(), field.ToString(), helpUrl)
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrorResponse">Specifies a client error response based on which to create the instance.</param>
        internal RequestUriTooLongException(ClientErrorResponse clientErrorResponse) : base(clientErrorResponse)
        {}

        #endregion
    }
}
