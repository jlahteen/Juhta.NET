
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Common
{
    /// <summary>
    /// A static class that defines the common diagnostic messages.
    /// </summary>
    public static class CommonMessages
    {
        #region Private Properties

        /// <summary>
        /// Gets the DiagnosticMessageFactory instance for creating diagnostic messages.
        /// </summary>
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(DiagnosticMessageIdBase.CommonMessages, typeof(CommonMessages).Namespace);

        #endregion

        #region Public Properties

        /// <summary>
        /// Value of the parameter '{0}' cannot be null.
        /// </summary>
        public static readonly ErrorMessage Error001 = MessageFactory.CreateErrorMessage("Value of the parameter '{0}' cannot be null.");

        /// <summary>
        /// Value of the parameter '{0}' cannot be an empty string or a string containing nothing but white space
        /// characters.
        /// </summary>
        public static readonly ErrorMessage Error002 = MessageFactory.CreateErrorMessage("Value of the parameter '{0}' cannot be an empty string or a string containing nothing but white space characters.");

        /// <summary>
        /// File '{0}' could not be locked within a {1} millisecond timeout.
        /// </summary>
        public static readonly ErrorMessage Error003 = MessageFactory.CreateErrorMessage("File '{0}' could not be locked within a {1} millisecond timeout.");

        /// <summary>
        /// Portion of the file '{0}' could not be locked within a {1} millisecond timeout.
        /// </summary>
        public static readonly ErrorMessage Error004 = MessageFactory.CreateErrorMessage("Portion of the file '{0}' could not be locked within a {1} millisecond timeout.");

        /// <summary>
        /// Value of the parameter '{0}' is not valid.
        /// </summary>
        public static readonly ErrorMessage Error005 = MessageFactory.CreateErrorMessage("Value of the parameter '{0}' is not valid.");

        /// <summary>
        /// Method '{0}' of the class '{1}' cannot be executed because the current state of the appropriate instance
        /// doesn't allow it.
        /// </summary>
        public static readonly ErrorMessage Error006 = MessageFactory.CreateErrorMessage("Method '{0}' of the class '{1}' cannot be executed because the current state of the appropriate instance doesn't allow it.");

        /// <summary>
        /// File '{0}' does not exist.
        /// </summary>
        public static readonly ErrorMessage Error007 = MessageFactory.CreateErrorMessage("File '{0}' does not exist.");

        /// <summary>
        /// Directory '{0}' does not exist.
        /// </summary>
        public static readonly ErrorMessage Error008 = MessageFactory.CreateErrorMessage("Directory '{0}' does not exist.");

        /// <summary>
        /// Method '{0}' is not supported by the type '{1}'.
        /// </summary>
        public static readonly ErrorMessage Error009 = MessageFactory.CreateErrorMessage("Method '{0}' is not supported by the type '{1}'.");

        /// <summary>
        /// Property '{0}' is not supported by the type '{1}'.
        /// </summary>
        public static readonly ErrorMessage Error010 = MessageFactory.CreateErrorMessage("Property '{0}' is not supported by the type '{1}'.");

        /// <summary>
        /// An error occurred in the closing process of the library '{0}': {1}
        /// </summary>
        public static readonly ErrorMessage Error011 = MessageFactory.CreateErrorMessage("An error occurred in the closing process of the library '{0}': {1}");

        /// <summary>
        /// Static method '{0}' of the class '{1}' cannot be executed because the current state of the class doesn't
        /// allow it.
        /// </summary>
        public static readonly ErrorMessage Error012 = MessageFactory.CreateErrorMessage("Static method '{0}' of the class '{1}' cannot be executed because the current state of the class doesn't allow it.");

        /// <summary>
        /// The lenghts of the arrays '{0}' and '{1}' do not match.
        /// </summary>
        public static readonly ErrorMessage Error013 = MessageFactory.CreateErrorMessage("The lenghts of the arrays '{0}' and '{1}' do not match.");

        #endregion
    }
}
