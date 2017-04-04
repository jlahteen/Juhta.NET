
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Common
{
    /// <summary>
    /// This exception will be thrown when a desired assembly was not found among the loaded assemblies of an
    /// application domain.
    /// </summary>
    public class AssemblyNotLoadedException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public AssemblyNotLoadedException(string message) : base(message)
        {}

        #endregion
    }
}
