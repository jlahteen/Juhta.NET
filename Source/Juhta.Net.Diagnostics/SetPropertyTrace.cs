
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a trace element that stores trace data related to a single execution of a set-property of a class.
    /// </summary>
    internal class SetPropertyTrace : MemberTrace
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parentElement">Specifies a TraceElement object that will be set as the parent element for the
        /// instance.</param>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="propertyName">Specifies a property name.</param>
        public SetPropertyTrace(TraceElement parentElement, string className, string propertyName) : base(parentElement, className, propertyName)
        {
            m_description = BuildDescription(className, propertyName);

            m_startLine = String.Format("SetProperty Start: {0}.{1}", className, propertyName);

            m_endLine = String.Format("SetProperty End: {0}.{1}", className, propertyName);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds a description for an instance of the SetPropertyTrace class.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="propertyName">Specifies a property name.</param>
        /// <returns>Returns the built description.</returns>
        public static string BuildDescription(string className, string propertyName)
        {
            return(String.Format("Execution of the set-property {0}.{1}", className, propertyName));
        }

        #endregion
    }
}
