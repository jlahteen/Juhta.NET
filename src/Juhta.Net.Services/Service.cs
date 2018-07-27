﻿
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            try
            {
                return(ObjectFactory.CreateInstance<TService>(m_classId, GetConstructorParams()));
            }

            catch (Exception ex)
            {
                throw new ServiceCreationException(LibraryMessages.Error004.FormatMessage(this.Id.Value), ex);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the identifier of the class that implements the dependency injection service.
        /// </summary>
        public ClassId ClassId
        {
            get {return(m_classId);}
        }

        /// <summary>
        /// Gets an array of <see cref="ConstructorParam"/> objects specifying the constructor parameters for the
        /// dependency injection service. Can be null.
        /// </summary>
        public ConstructorParam[] ConstructorParams
        {
            get {return(m_constructorParams);}
        }

        /// <summary>
        /// Gets the identifier of the dependency injection service.
        /// </summary>
        public ServiceId Id
        {
            get {return(m_id);}
        }

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serviceFactory">Specifies a <see cref="ServiceFactory"/> object that will be set to own the
        /// instance.</param>
        /// <param name="serviceNode">Specifies a service XML node based on which to initialize the instance.</param>
        internal Service(ServiceFactory serviceFactory, XmlNode serviceNode)
        {
            XmlNode constructorParamsNode;
            XmlNamespaceManager namespaceManager = FrameworkConfig.CreateRootConfigNamespaceManager(serviceNode.OwnerDocument);
            List<ConstructorParam> constructorParams = new List<ConstructorParam>();

            m_serviceFactory = serviceFactory;

            m_id = new ServiceId(serviceNode.GetAttribute("id"));

            try
            {
                m_classId = new ClassId(serviceNode.GetAttribute("class"), Application.Instance.BinDirectory);

                constructorParamsNode = serviceNode.SelectSingleNode("ns:constructorParams", namespaceManager);

                if (constructorParamsNode == null)
                    return;

                foreach (XmlNode paramNode in constructorParamsNode.ChildNodes)
                    constructorParams.Add(new ConstructorParam(paramNode));

                if (constructorParams.Count == 0)
                    return;

                m_constructorParamObjs = new object[constructorParams.Count];

                for (int i = 0; i < m_constructorParamObjs.Length; i++)
                    m_constructorParamObjs[i] = constructorParams[i].Value;

                m_constructorParams = constructorParams.ToArray();

                m_hasServiceParams = m_constructorParams.Where(x => x.Type == ConstructorParamType.Service).Count() > 0;
            }

            catch (Exception ex)
            {
                throw new ServiceInitializationException(LibraryMessages.Error005.FormatMessage(this.Id.Value), ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an array of the constructor parameter objects required for creating an instance of the service.
        /// </summary>
        /// <returns>Returns an array of the required constructor parameter objects.</returns>
        private object[] GetConstructorParams()
        {
            object[] constructorParams;

            if (!m_hasServiceParams)
                return(m_constructorParamObjs);

            constructorParams = new object[m_constructorParams.Length];

            for (int i = 0; i < m_constructorParams.Length; i++)
                if (m_constructorParams[i].Type != ConstructorParamType.Service)
                    constructorParams[i] = m_constructorParams[i].Value;
                else
                    constructorParams[i] = m_serviceFactory.CreateService<object>((ServiceId)m_constructorParams[i].Value);

            return(constructorParams);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="ClassId"/> property.
        /// </summary>
        private ClassId m_classId;

        /// <summary>
        /// Stores the <see cref="ConstructorParams"/> property.
        /// </summary>
        private ConstructorParam[] m_constructorParams;

        /// <summary>
        /// Specifies an array of the constructor parameters for creating instances of the service. Can be null.
        /// </summary>
        private object[] m_constructorParamObjs;

        /// <summary>
        /// Specifies whether the constructor parameters of this service contain service type parameters.
        /// </summary>
        private bool m_hasServiceParams;

        /// <summary>
        /// Specifies the <see cref="ServiceFactory"/> object that owns this <see cref="Service"/> instance.
        /// </summary>
        private ServiceFactory m_serviceFactory;

        /// <summary>
        /// Stores the <see cref="Id"/> property.
        /// </summary>
        private ServiceId m_id;

        #endregion
    }
}
