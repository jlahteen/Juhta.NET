﻿
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines a class for serializing instances of <see cref="ServerErrorException"/>.
    /// </summary>
    public abstract class ServerError : WebApiError
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the inner exception that relates to the server error.
        /// </summary>
        /// <remarks>Please note that an inner exception will be set as a string.</remarks>
        public string InnerException {get; set;}

        #endregion
    }
}
