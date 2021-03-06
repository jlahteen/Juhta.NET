﻿
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.WebApi.Exceptions.ServerErrors;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines a class for serializing instances of <see cref="ServerErrorException"/>.
    /// </summary>
    public class ServerErrorResponse : WebApiErrorResponse
    {
        #region Public Methods

        /// <summary>
        /// Throws this <see cref="ServerErrorResponse"/> as a corresponding exception derived from
        /// <see cref="ServerErrorException"/>.
        /// </summary>
        public override void Throw()
        {
            HttpStatusCode statusCode;

            statusCode = (HttpStatusCode)System.Enum.Parse(typeof(HttpStatusCode), this.StatusCode.Substring(this.StatusCode.IndexOf('.') + 1));

            switch (statusCode)
            {
                case HttpStatusCode.BadGateway:
                    throw new BadGatewayException(this);

                case HttpStatusCode.GatewayTimeout:
                    throw new GatewayTimeoutException(this);

                case HttpStatusCode.HttpVersionNotSupported:
                    throw new HttpVersionNotSupportedException(this);

                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException(this);

                case HttpStatusCode.NotImplemented:
                    throw new NotImplementedException(this);

                case HttpStatusCode.ServiceUnavailable:
                    throw new ServiceUnavailableException(this);

                default:
                    throw new BlockNotImplementedException(this.StatusCode);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the error message that relates to the server error.
        /// </summary>
        public string ErrorMessage {get; set;}

        #endregion
    }
}
