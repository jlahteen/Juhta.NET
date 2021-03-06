
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for initializable libraries. A library is initializable if it requires initialization
    /// operations and those operations don't need any configuration.
    /// </summary>
    public interface IInitializableLibrary
    {
        #region Methods

        /// <summary>
        /// Initializes the library, that is, performs required initialization operations to make library services
        /// properly available.
        /// </summary>
        void InitializeLibrary();

        #endregion
    }
}
