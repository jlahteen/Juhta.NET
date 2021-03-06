﻿
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines an abstract base class for the server error Web API exceptions.
    /// </summary>
    public abstract class ServerErrorException : WebApiException
    {
        #region Public Methods

        /// <summary>
        /// Converts this <see cref="ServerErrorException"/> object to a <see cref="ServerErrorResponse"/> object.
        /// </summary>
        /// <returns>Returns the resulting <see cref="ServerErrorResponse"/> object.</returns>
        public ServerErrorResponse ToServerErrorResponse()
        {
            ServerErrorResponse serverErrorResponse = new ServerErrorResponse();

            serverErrorResponse.ServiceStack = this.ServiceStack;

            serverErrorResponse.ErrorMessage = this.ErrorMessage;

            serverErrorResponse.StatusCode = "ServerError." + this.StatusCode.ToString();

            return(serverErrorResponse);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the error message related to this <see cref="ServerErrorException"/> instance.
        /// </summary>
        public string ErrorMessage
        {
            get {return(m_errorMessage);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        protected ServerErrorException(HttpStatusCode httpStatusCode) : this(httpStatusCode, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serverErrorResponse">Specifies a server error.</param>
        protected ServerErrorException(ServerErrorResponse serverErrorResponse) : base(serverErrorResponse, serverErrorResponse.ErrorMessage)
        {
            m_errorMessage = serverErrorResponse.ErrorMessage;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        protected ServerErrorException(HttpStatusCode httpStatusCode, string message) : this(httpStatusCode, message, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception.</param>
        protected ServerErrorException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(httpStatusCode, message, innerException)
        {
            m_errorMessage = message;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="ErrorMessage"/> property.
        /// </summary>
        private string m_errorMessage;

        #endregion
    }
}
