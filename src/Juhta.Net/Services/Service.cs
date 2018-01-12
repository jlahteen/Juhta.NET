﻿
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Extensions;
using System.Collections.Generic;
using System.Xml;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines a class that encapsulates the metadata of a dependency injection service.
    /// </summary>
    public class Service
    {
        #region Public Methods

        /// <summary>
        /// Creates an instance of the encapsulated dependency injection service.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <returns>Returns the created instance casted to the specified service type.</returns>
        public TService CreateInstance<TService>() where TService : class
        {
            return(ObjectFactory.CreateInstance<TService>(m_libraryFileName, m_libraryClass, m_constructorParamObjs));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets an array of <see cref="Param"/> objects specifying the constructor parameters for the dependency
        /// injection service. Can be null.
        /// </summary>
        public Param[] ConstructorParams
        {
            get {return(m_constructorParams);}
        }

        /// <summary>
        /// Gets the library class that implements the dependency injection service.
        /// </summary>
        public string LibraryClass
        {
            get {return(m_libraryClass);}
        }

        /// <summary>
        /// Gets the file name of the library that contains the dependency injection service.
        /// </summary>
        public string LibraryFileName
        {
            get {return(m_libraryFileName);}
        }

        /// <summary>
        /// Gets the name of the dependency injection service.
        /// </summary>
        public string Name
        {
            get {return(m_name);}
        }

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serviceNode">Specifies a service XML node based on which to initialize the instance.</param>
        internal Service(XmlNode serviceNode)
        {
            XmlNode constructorNode;
            XmlNamespaceManager namespaceManager = FrameworkConfig.CreateRootConfigNamespaceManager(serviceNode.OwnerDocument);
            List<Param> constructorParams = new List<Param>();

            m_name = serviceNode.GetAttribute("name");

            m_libraryFileName = serviceNode.GetAttribute("libraryFileName");

            m_libraryClass = serviceNode.GetAttribute("libraryClass");

            constructorNode = serviceNode.SelectSingleNode("//ns:constructor", namespaceManager);

            if (constructorNode == null)
                return;

            foreach (XmlNode paramNode in constructorNode.ChildNodes)
                constructorParams.Add(new Param(paramNode));

            if (constructorParams.Count == 0)
                return;

            m_constructorParamObjs = new object[constructorParams.Count];

            for (int i = 0; i < m_constructorParamObjs.Length; i++)
                m_constructorParamObjs[i] = constructorParams[i].Value;

            m_constructorParams = constructorParams.ToArray();
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="ConstructorParams"/> property.
        /// </summary>
        private Param[] m_constructorParams;

        /// <summary>
        /// Specifies an array of the constructor parameters for creating instances of the service. Can be null.
        /// </summary>
        private object[] m_constructorParamObjs;

        /// <summary>
        /// Stores the <see cref="LibraryClass"/> property.
        /// </summary>
        private string m_libraryClass;

        /// <summary>
        /// Stores the <see cref="LibraryFileName"/> property.
        /// </summary>
        private string m_libraryFileName;

        /// <summary>
        /// Stores the <see cref="Name"/> property.
        /// </summary>
        private string m_name;

        #endregion
    }
}
