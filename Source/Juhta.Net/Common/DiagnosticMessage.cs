
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines an abstract base class for diagnostic messages. Diagnostic messages make it possible not to write long
    /// messages inside the actual code but centralize them in better manageable separate contexts.
    /// </summary>
    public abstract class DiagnosticMessage
    {
        #region Public Methods

        /// <summary>
        /// Formats the message stored in this DiagnosticMessage instance with specified objects.
        /// </summary>
        /// <param name="args">Specifies an array of objects to format. Can be null.</param>
        /// <returns>Returns the formatted message. If the specified object array doesn't match the format items in the
        /// message stored in this DiagnosticMessage instance, the method just returns the message without any
        /// formatting.</returns>
        public string FormatMessage(params object[] args)
        {
            string message = null;

            try
            {
                if (args == null)
                    message = m_message;
                else
                    message = String.Format(m_message, args);
            }

            catch
            {
                message = m_message;
            }

            finally
            {
                SaveAsLastFormattedMessage(message);
            }

            return(message);
        }

        /// <summary>
        /// Gets an integer ID out of the <see cref="Id"/> property of this DiagnosticMessage instance.
        /// </summary>
        /// <returns>Returns the greatest whole number fragment in the <see cref="Id"/> property of this
        /// DiagnosticMessage instance as an integer. If there are no whole number fragments in the ID property, the
        /// return value is zero.</returns>
        /// <seealso cref="GetIntegerId(string)"/>
        public int GetIntegerId()
        {
            return(DiagnosticMessage.GetIntegerId(m_id));
        }

        /// <summary>
        /// Gets an integer ID out of a specified string ID.
        /// </summary>
        /// <param name="id">Specifies a string ID.</param>
        /// <returns>Returns the greatest whole number fragment in the specified string ID as an integer. If there are
        /// no whole number fragments in the string ID, the return value is zero.</returns>
        /// <seealso cref="GetIntegerId()"/>
        public static int GetIntegerId(string id)
        {
            int maxIntegerId = 0, integerId;
            string digits = null;
            string[] wholeNumberFragments;

            if (String.IsNullOrEmpty(id))
                return(maxIntegerId);

            foreach (Char c in id.ToCharArray())
                if (Char.IsDigit(c))
                    digits += c;
                else
                    digits += ' ';

            if (String.IsNullOrEmpty(digits = digits.Trim()))
                return(maxIntegerId);

            wholeNumberFragments = digits.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string wholeNumberFragment in wholeNumberFragments)
                if (Int32.TryParse(wholeNumberFragment, out integerId))
                    if (integerId > maxIntegerId)
                        maxIntegerId = integerId;

            return(maxIntegerId);
        }

        /// <summary>
        /// Gets the message stored in this DiagnosticMessage instance without any formatting.
        /// </summary>
        /// <returns>Returns the stored message without any formatting.</returns>
        /// <remarks>Use this method instead of <see cref="FormatMessage"/> when the message contains no format items.</remarks>
        public string GetMessage()
        {
            SaveAsLastFormattedMessage(m_message);

            return(m_message);
        }

        /// <summary>
        /// Searches the last formatted messages for the current thread, and checks whether a specified message matches
        /// one of them.
        /// </summary>
        /// <param name="message">Specifies a message to search.</param>
        /// <param name="messageId">If the function returns true, returns the ID of the specified message, otherwise
        /// returns null.</param>
        /// <returns>Returns true if the specified message was found among the last formatted messages for the current
        /// thread and the associated DiagnosticMessage object has a non-null ID.</returns>
        public static bool TryGetMessageId(string message, out string messageId)
        {
            messageId = null;

            if (t_lastFormattedMessages == null)
                return(false);

            foreach (KeyValuePair<string, DiagnosticMessage> keyValuePair in t_lastFormattedMessages.ToArray())
                if (keyValuePair.Key == message)
                {
                    messageId = keyValuePair.Value.Id;

                    break;
                }

            return(messageId != null);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the ID of the diagnostic message that this DiagnosticMessage instance represents.
        /// </summary>
        public string Id
        {
            get {return(m_id);}
        }

        /// <summary>
        /// Gets the message associated with this DiagnosticMessage instance.
        /// </summary>
        /// <remarks>The value can contain format items.</remarks>
        public string Message
        {
            get {return(m_message);}
        }

        /// <summary>
        /// Gets the type of the diagnostic message that this DiagnosticMessage instance represents.
        /// </summary>
        public DiagnosticMessageType Type
        {
            get {return(m_type);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type">Specifies a value for the <see cref="Type"/> property.</param>
        /// <param name="message">Specifies a message that will be associated with the instance. The value can contain
        /// format items.</param>
        protected DiagnosticMessage(DiagnosticMessageType type, string message) : this(type, message, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type">Specifies a value for the <see cref="Type"/> property.</param>
        /// <param name="message">Specifies a message that will be associated with the instance. The value can contain
        /// format items.</param>
        /// <param name="id">Specifies a value for the <see cref="Id"/> property.</param>
        protected DiagnosticMessage(DiagnosticMessageType type, string message, string id)
        {
            m_type = type;

            m_message = message;

            m_id = id;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves a specified message as a last formatted message for the current thread, and associates the current
        /// DiagnosticMessage instance with it.
        /// </summary>
        /// <param name="message">Specifies a message.</param>
        private void SaveAsLastFormattedMessage(string message)
        {
            try
            {
                if (t_lastFormattedMessages == null)
                    t_lastFormattedMessages = new Queue<KeyValuePair<string, DiagnosticMessage>>();

                if (t_lastFormattedMessages.Count == c_maxLastFormattedMessagesCount)
                    t_lastFormattedMessages.Dequeue();

                t_lastFormattedMessages.Enqueue(new KeyValuePair<string, DiagnosticMessage>(message, this));
            }

            catch {}
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the maximum length for the last formatted messages queue per thread.
        /// </summary>
        private const int c_maxLastFormattedMessagesCount = 64;

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Id"/> property.
        /// </summary>
        private string m_id;

        /// <summary>
        /// Specifies the thread-specific queue for the last formatted messages. Queue items are key/value pairs
        /// consisting of a formatted message and the corresponding DiagnosticMessage instance.
        /// </summary>
        [ThreadStatic]
        private static Queue<KeyValuePair<string, DiagnosticMessage>> t_lastFormattedMessages;

        /// <summary>
        /// Stores the <see cref="Message"/> property.
        /// </summary>
        private string m_message;

        /// <summary>
        /// Stores the <see cref="Type"/> property.
        /// </summary>
        private DiagnosticMessageType m_type;

        #endregion
    }
}
