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
    /// Defines an abstract base class for serializing instances of <see cref="WebApiException"/>.
    /// </summary>
    public abstract class WebApiError
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the call stack of the Web API error.
        /// </summary>
        public string[] CallStack {get; set;}

        /// <summary>
        /// Gets or sets the error message of the Web API error.
        /// </summary>
        public string ErrorMessage {get; set;}

        /// <summary>
        /// Gets or sets the HTTP status code of the Web API error.
        /// </summary>
        /// <remarks>HTTP status codes will be serialized into this field by using textual values of the enumeration
        /// <see cref="HttpStatusCode"/>. Values will also be prefixed by 'ClientError.' or 'ServerError.'. For
        /// example, 'ClientError.BadRequest' and 'ServerError.BadGateway' are possible values.</remarks>
        public string StatusCode {get; set;}

        #endregion
    }
}
