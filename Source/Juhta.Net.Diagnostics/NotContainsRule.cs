
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Xml;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines the NotContainsRule filter rule.
    /// </summary>
    internal class NotContainsRule : ContainsRule
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="notContainsRuleNode">Specifies a notContainsRule XML node based on which the instance will be
        /// initialized.</param>
        public NotContainsRule(XmlNode notContainsRuleNode) : base(notContainsRuleNode)
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IFilterRule.IsValidFor"/>.
        /// </summary>
        public override bool IsValidFor(Event @event)
        {
            return(!base.IsValidFor(@event));
        }

        #endregion
    }
}
