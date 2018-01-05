﻿
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;

namespace Juhta.Net.Services
{
    public class Service
    {
        #region Static Constructor
        #endregion

        #region Public Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name
        {
            get {return(m_name);}
        }

        #endregion

        #region Public Indexers
        #endregion

        #region Public Types
        #endregion

        #region Public Constants
        #endregion

        #region Protected Constructors
        #endregion

        #region Protected Methods
        #endregion

        #region Protected Properties
        #endregion

        #region Protected Types
        #endregion

        #region Protected Fields
        #endregion

        #region Internal Constructors

        internal Service(XmlNode serviceNode)
        {

        }

        #endregion

        #region Internal Methods
        #endregion

        #region Internal Properties
        #endregion

        #region Internal Types
        #endregion

        #region Private Methods
        #endregion

        #region Private Types
        #endregion

        #region Private Constants
        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Name"/> property.
        /// </summary>
        private string m_name;

        #endregion

        #region Destructor
        #endregion
    }
}
