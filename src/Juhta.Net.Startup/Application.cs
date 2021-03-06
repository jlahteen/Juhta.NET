
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Diagnostics;
using Juhta.Net.Extensions;
using Juhta.Net.Framework;
using Juhta.Net.LibraryManagement;
using Juhta.Net.Validation;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Juhta.Net.Startup
{
    /// <summary>
    /// A class that represents an application built on the top of the framework. The class provides basic information
    /// about the application and methods for initializing and closing the application.
    /// </summary>
    public class Application : Singleton<Application>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <remarks>Log events will be written to the current user's temp directory, and the configuration files are
        /// assumed to locate in the binary directory.</remarks>
        public Application() : this(null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies an invalid log file.</param>
        /// <remarks>The configuration files are assumed to locate in the binary directory.</remarks>
        public Application(string logFilePath) : this(logFilePath, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies an invalid log file.</param>
        /// <param name="configDirectory">Specifies a directory to search for configuration files. Can be null in which
        /// case the configuration files are assumed to locate in the binary directory.</param>
        public Application(string logFilePath, string configDirectory)
        {
            // Set the singleton instance
            SetSingletonInstance(this);

            // Create a logger instance
            Logger.SetLogger(new FileLogger(logFilePath));

            // Set the binary directory
            m_binDirectory = FrameworkInfo.BinDirectory;

            // Use the binary directory as the configuration directory if necessary
            if (String.IsNullOrEmpty(configDirectory))
                configDirectory = m_binDirectory;

            // Ensure the full path in the configuration directory
            configDirectory = Path.GetFullPath(configDirectory);

            // Set the configuration directory
            m_configDirectory = configDirectory.TrimEnd(Path.DirectorySeparatorChar);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes the application by closing all configured startup libraries.
        /// </summary>
        public void Close()
        {
            // Check the state of the application
            if (m_state == State.Uninitialized)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage("Close", typeof(Application)));

            try
            {
                if (m_libraryManager != null)
                {
                    // Stop watching configuration file changes
                    m_libraryManager.StopConfigFileWatching();

                    // Close the libraries
                    m_libraryManager.CloseLibraries();
                }

                // Reset the singleton instance
                ResetSingletonInstance();
            }

            catch (Exception ex)
            {
                // We don't expect exceptions but such occurred anyway
                Logger.LogError(ex, LibraryMessages.Error018, this.Name);
            }

            finally
            {
                // Update the state of the application
                m_state = State.Uninitialized;

                // Reset the rest of the static fields

                m_binDirectory = null;

                m_configDirectory = null;

                m_libraryManager = null;
            }
        }

        /// <summary>
        /// Closes the possibly created singleton <see cref="Application"/> instance.
        /// </summary>
        public static void CloseInstance()
        {
            if (Application.Instance == null)
                return;

            else if (Application.Instance.IsInitialized)
                Application.Instance.Close();

            else
                Application.Instance.ResetSingletonInstance();
        }

        /// <summary>
        /// Creates an instance of the singleton <see cref="Application"/> class.
        /// </summary>
        /// <returns>Returns the created instance of <see cref="Application"/>.</returns>
        public static Application CreateInstance()
        {
            return(new Application());
        }

        /// <summary>
        /// Creates an instance of <see cref="DynamicLibraryContext{TDynamicLibrary, TLibraryState}"/> corresponding to
        /// specified dynamic library type and library state type.
        /// </summary>
        /// <typeparam name="TDynamicLibrary">Specifies a dynamic library type.</typeparam>
        /// <typeparam name="TLibraryState">Specifies a library state type.</typeparam>
        /// <returns>Returns the created <see cref="DynamicLibraryContext{TDynamicLibrary, TLibraryState}"/> instance.</returns>
        public DynamicLibraryContext<TDynamicLibrary, TLibraryState> GetDynamicLibraryContext<TDynamicLibrary, TLibraryState>()
            where TDynamicLibrary : IDynamicLibrary
            where TLibraryState : ILibraryState
        {
            return(m_libraryManager.GetDynamicLibraryContext<TDynamicLibrary, TLibraryState>());
        }

        /// <summary>
        /// Sets the state of the application as <see cref="State.Initialized"/>.
        /// </summary>
        /// <remarks>
        /// <para>This method is used for marking the application 'ready' after calling one or more initialization
        /// methods typically with method chaining.</para>
        /// <para>This method is not allowed if the application has already been initialized.</para>
        /// </remarks>
        public void Ready()
        {
            // Check the state of the application
            if (m_state > State.Initializing)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage(nameof(Ready), typeof(Application)));

            m_state = State.Initialized;
        }

        /// <summary>
        /// Sets the name of the application.
        /// </summary>
        /// <param name="name">Specifies an application name.</param>
        /// <returns>Returns the current <see cref="Application"/> instance allowing startups with method chaining.</returns>
        /// <remarks>
        /// <para>The new name always replaces any previously set name.</para>
        /// <para>This method is not allowed if the application has already been initialized.</para>
        /// </remarks>
        public Application SetName(string name)
        {
            SetStateInitializing(nameof(SetName));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException(LibraryMessages.Error015.GetMessage());

            m_name = name.Trim();

            return(this);
        }

        /// <summary>
        /// Starts the application by initializing all configured startup libraries.
        /// </summary>
        public void Start()
        {
            XmlDocument config;
            XmlNamespaceManager namespaceManager;

            // Check the current state of the application
            if (m_state > State.Uninitialized)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage("Start", typeof(Application)));

            try
            {
                // Perform the initialization if necessary

                if ((config = LoadAndValidateConfig()) != null)
                {
                    // Create a namespace manager for the configuration
                    namespaceManager = FrameworkConfig.CreateNamespaceManager(GetType().Assembly.GetFileName(), config, "v1");

                    // Initialize the attribute fields
                    InitializeAttributeFields(config.SelectSingleNode("//ns:application", namespaceManager));

                    // Initialize the libraries

                    m_libraryManager = new LibraryManager(this);

                    m_libraryManager.InitializeLibraries(config.SelectSingleNode("//ns:application/ns:libraries", namespaceManager));

                    // Start watching configuration file changes
                    m_libraryManager.StartConfigFileWatching();
                }

                // Update the state of the application
                m_state = State.Initialized;
            }

            catch (Exception ex)
            {
                // Log the exception
                Logger.LogError(ex, LibraryMessages.Error006, this.Name);

                // Log an alert
                Logger.LogAlert(LibraryMessages.Alert007, this.Name);

                // Update the state of the application
                m_state = State.PartlyInitialized;

                // Rethrow the exception
                throw;
            }
        }

        /// <summary>
        /// Creates and starts a new singleton <see cref="Application"/> instance.
        /// </summary>
        /// <remarks>Log events will be written to the current user's temp directory, and the configuration files are
        /// assumed to locate in the binary directory.</remarks>
        public static void StartInstance()
        {
            StartInstance(null, null);
        }

        /// <summary>
        /// Creates and starts a new singleton <see cref="Application"/> instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies an invalid log file.</param>
        /// <remarks>The configuration files are assumed to locate in the binary directory.</remarks>
        public static void StartInstance(string logFilePath)
        {
            StartInstance(logFilePath, null);
        }

        /// <summary>
        /// Creates and starts a new singleton <see cref="Application"/> instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies an invalid log file.</param>
        /// <param name="configDirectory">Specifies a directory to search for configuration files. Can be null in which
        /// case the configuration files are assumed to locate in the binary directory.</param>
        public static void StartInstance(string logFilePath, string configDirectory)
        {
            Application application = new Application(logFilePath, configDirectory);

            application.Start();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the binary directory for the framework and application libraries. The return value is null if the
        /// application is not initialized.
        /// </summary>
        public string BinDirectory
        {
            get {return(m_binDirectory);}
        }

        /// <summary>
        /// Gets the configuration directory for the framework and application libraries. The return value is null if
        /// the application is not initialized.
        /// </summary>
        public string ConfigDirectory
        {
            get {return(m_configDirectory);}
        }

        /// <summary>
        /// Gets the name of the default configuration file for the application. Can be null.
        /// </summary>
        public string DefaultConfigFileName
        {
            get {return(m_defaultConfigFileName);}
        }

        /// <summary>
        /// Returns true if the application has been initialized, otherwise false.
        /// </summary>
        public bool IsInitialized
        {
            get {return(m_state > State.Uninitialized);}
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        public string Name
        {
            get
            {
                if (m_name == null)
                    m_name = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

                return(m_name);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes those fields that are determined by the attributes of the application XML node.
        /// </summary>
        /// <param name="applicationNode">Specifies an application XML node based on which to initialize the attribute
        /// fields.</param>
        private void InitializeAttributeFields(XmlNode applicationNode)
        {
            if (applicationNode.HasAttribute("name"))
                m_name = applicationNode.GetAttribute("name");
            else
                m_name = this.Name;

            if (applicationNode.HasAttribute("defaultConfigFileName"))
                m_defaultConfigFileName = applicationNode.GetAttribute("defaultConfigFileName");
        }

        /// <summary>
        /// Loads and validates the configuration file.
        /// </summary>
        /// <returns>Returns an <see cref="XmlDocument"/> object containing the configuration. If there is no
        /// configuration file, the return value is null.</returns>
        private XmlDocument LoadAndValidateConfig()
        {
            string configFilePath;
            XmlDocument config = null;
            XmlDocumentValidator configValidator;

            configFilePath = String.Format("{0}{1}{2}.config", m_configDirectory, Path.DirectorySeparatorChar, typeof(Application).Namespace);

            if (!File.Exists(configFilePath))
                return(null);

            config = new XmlDocument();

            config.Load(configFilePath);

            configValidator = new XmlDocumentValidator();

            configValidator.AddSchema(FrameworkConfig.GetEmbeddedConfigSchema(Assembly.GetExecutingAssembly()));

            configValidator.AddSchema(FrameworkConfig.GetEmbeddedCommonConfigSchema());

            try
            {
                configValidator.Validate(config);
            }

            catch (ValidationException ex)
            {
                throw new InvalidConfigFileException(LibraryMessages.Error002.FormatMessage(configFilePath, typeof(Application).Namespace + ".dll"), ex);
            }

            return(config);
        }

        /// <summary>
        /// Sets the state of the application as <see cref="State.Initializing"/>.
        /// </summary>
        /// <param name="callingMethod">Specifies the name of a calling method.</param>
        /// <remarks>If the current state of the application is greater than <see cref="State.Initializing"/>, an
        /// <see cref="InvalidOperationException"/> will be thrown.</remarks>
        private void SetStateInitializing(string callingMethod)
        {
            if (m_state > State.Initializing)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage(callingMethod, typeof(Application)));

            m_state = State.Initializing;
        }

        #endregion

        #region Private Types

        /// <summary>
        /// Defines an enumeration for the states of the application.
        /// </summary>
        private enum State
        {
            /// <summary>
            /// The application is uninitialized.
            /// </summary>
            Uninitialized,

            /// <summary>
            /// The application is currently being initialized (typically with method chaining).
            /// </summary>
            Initializing,

            /// <summary>
            /// The application is only partly initialized, that is, the initialization has not been completed due to
            /// an error.
            /// </summary>
            PartlyInitialized,

            /// <summary>
            /// The application has been successfully initialized.
            /// </summary>
            Initialized
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="BinDirectory"/> property.
        /// </summary>
        private string m_binDirectory;

        /// <summary>
        /// Stores the <see cref="ConfigDirectory"/> property.
        /// </summary>
        private string m_configDirectory;

        /// <summary>
        /// Stores the <see cref="DefaultConfigFileName"/> property.
        /// </summary>
        private string m_defaultConfigFileName;

        /// <summary>
        /// Specifies the <see cref="LibraryManager"/> instance that was created when the application was initialized.
        /// </summary>
        private LibraryManager m_libraryManager;

        /// <summary>
        /// Stores the <see cref="Name"/> property.
        /// </summary>
        private string m_name;

        /// <summary>
        /// Specifies the current state of the application.
        /// </summary>
        private State m_state;

        #endregion
    }
}
