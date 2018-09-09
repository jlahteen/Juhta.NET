﻿
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Net;

namespace Juhta.Net.WebApi.Exceptions.ServerErrors
{
    /// <summary>
    /// Defines an exception class for the HTTP error Internal Server Error.
    /// </summary>
    public class InternalServerErrorException : ServerErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public InternalServerErrorException() : base(HttpStatusCode.InternalServerError)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public InternalServerErrorException(string message) : base(HttpStatusCode.InternalServerError, message)
        {}

        #endregion
    }
}
