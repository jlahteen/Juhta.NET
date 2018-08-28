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
    /// Defines an exception class for the HTTP error Expectation Failed.
    /// </summary>
    public class ExpectationFailedException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ExpectationFailedException() : base(HttpStatusCode.ExpectationFailed)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public ExpectationFailedException(string message) : base(HttpStatusCode.ExpectationFailed, message)
        {}

        #endregion
    }
}
