
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// This exception will be thrown when an error occurs in a closing of a library instance.
    /// </summary>
    public class LibraryClosingException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception that is the actual cause for the library closing
        /// error.</param>
        public LibraryClosingException(string message, Exception innerException) : base(message, innerException)
        {}

        #endregion
    }
}
