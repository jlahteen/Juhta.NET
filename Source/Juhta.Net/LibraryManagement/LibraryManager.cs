
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// A class that represents a library manager. The library manager is responsible for managing the whole life cycle
    /// of libraries including initialization, dynamic configuration changes and closing.
    /// </summary>
    internal class LibraryManager
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public LibraryManager()
        {
            m_dynamicLibraries = new Dictionary<string, List<IDynamicLibrary>>();

            m_libraryHandles = new Stack<ILibraryHandle>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes all libraries.
        /// </summary>
        /// <remarks>
        /// <para>After this method has been called, libraries are no longer properly initialized.</para>
        /// <para>This method will be called by the framework manager when the framework is closed.</para>
        /// </remarks>
        public void CloseLibraries()
        {
            ILibraryHandle libraryHandle;

            // Close all libraries one by one

            while (m_libraryHandles.Count > 0)
            {
                try
                {
                    libraryHandle = m_libraryHandles.Pop();

                    if (libraryHandle is IDynamicLibrary)
                        CloseDynamicLibrary((IDynamicLibrary)libraryHandle);
                    else
                        CloseLibrary(libraryHandle);
                }

                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            // Clear all library collections stored by this class
            ClearLibraryCollections();
        }

        /// <summary>
        /// Initializes the libraries listed in a specified root configuration.
        /// </summary>
        /// <param name="rootConfig">Specifies an <see cref="XmlDocument"/> object containing the root configuration.</param>
        /// <param name="namespaceManager">Specifies an <see cref="XmlNamespaceManager"/> object for selecting nodes in
        /// <paramref name="rootConfig"/>.</param>
        public void InitializeLibraries(XmlDocument rootConfig, XmlNamespaceManager namespaceManager)
        {
            XmlNode librariesNode;
            ILibraryHandle libraryHandle;

            // Select the libraries XML node
            librariesNode = rootConfig.SelectSingleNode("//ns:libraries", namespaceManager);

            // Initialize the libraries listed in the libraries XML node

            foreach (XmlNode libraryNode in librariesNode)
            {
                libraryHandle = LibraryHandle.CreateInstance(libraryNode);

                InitializeLibrary(libraryHandle);
            }
        }

        /// <summary>
        /// Starts an asynchronous watching of configuration file changes.
        /// </summary>
        public void StartConfigFileWatching()
        {
            // Create a configuration file watcher
            m_configFileWatcher = new ConfigFileWatcher();

            // Subscribe to the ConfigFileCreated, ConfigFileChanged and ConfigFileDeleted events
            m_configFileWatcher.ConfigFileCreated += OnConfigFileCreatedOrChanged;
            m_configFileWatcher.ConfigFileChanged += OnConfigFileCreatedOrChanged;
            m_configFileWatcher.ConfigFileDeleted += OnConfigFileDeleted;

            // Start watching configuration file changes
            m_configFileWatcher.StartWatching(Startup.ConfigDirectory);
        }

        /// <summary>
        /// Stops the asynchronous watching of configuration file changes.
        /// </summary>
        public void StopConfigFileWatching()
        {
            // Return if there is no configuration file watcher
            if (m_configFileWatcher == null)
                return;

            // Stop watching configuration file changes
            m_configFileWatcher.StopWatching();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Clears all library collections of the class.
        /// </summary>
        private void ClearLibraryCollections()
        {
            m_dynamicLibraries.Clear();

            m_libraryHandles.Clear();
        }

        /// <summary>
        /// Closes a specified dynamic library.
        /// </summary>
        /// <param name="library"></param>
        private void CloseDynamicLibrary(IDynamicLibrary library)
        {
            ILibraryState libraryState;

            library.LibraryStateLock.EnterWriteLock();

            try
            {
                libraryState = library.LibraryState;

                StopLibraryStateProcesses(libraryState, false);

                CloseLibraryState(libraryState);

                library.LibraryState = null;
            }

            finally
            {
                library.LibraryStateLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Closes a specified non-dynamic library.
        /// </summary>
        /// <param name="libraryHandle">Specifies a library handle.</param>
        private static void CloseLibrary(ILibraryHandle libraryHandle)
        {
            IStartableLibrary startableLibrary;
            IClosableLibrary closableLibrary;

            try
            {
                // Stop the library processes if necessary

                startableLibrary = libraryHandle as IStartableLibrary;

                if (startableLibrary != null && !startableLibrary.StopProcesses())
                    Logger.LogWarning(LibraryMessages.Warning072, libraryHandle.LibraryFileName);
            }

            catch (Exception ex)
            {
                Logger.LogError(LibraryMessages.Error071.FormatMessage(libraryHandle.LibraryFileName, ex));
            }

            try
            {
                // Close the library

                closableLibrary = libraryHandle as IClosableLibrary;

                if (closableLibrary == null)
                    return;

                else if (!closableLibrary.CloseLibrary())
                    Logger.LogWarning(LibraryMessages.Warning017, libraryHandle.LibraryFileName);
            }

            catch (Exception ex)
            {
                Logger.LogError(LibraryMessages.Error004.FormatMessage(libraryHandle.LibraryFileName), ex);
            }
        }

        /// <summary>
        /// Closes a library state if necessary.
        /// </summary>
        /// <param name="libraryState">Specifies a library state. Can be null.</param>
        private void CloseLibraryState(ILibraryState libraryState)
        {
            IClosableLibraryState closableLibraryState;

            if (libraryState == null)
                return;

            closableLibraryState = libraryState as IClosableLibraryState;

            if (closableLibraryState == null)
                // There is nothing to close
                return;

            try
            {
                if (!closableLibraryState.Close())
                    Logger.LogWarning(LibraryMessages.Warning076.FormatMessage(GetLibraryFileName(libraryState.LibraryHandle)));
            }

            catch (Exception ex)
            {
                Logger.LogError(LibraryMessages.Error077.FormatMessage(GetLibraryFileName(libraryState.LibraryHandle), ex));
            }
        }

        /// <summary>
        /// Creates the default library state for a specified dynamic library.
        /// </summary>
        /// <param name="library">Specifies a dynamic library.</param>
        /// <returns>Returns a library state object that holds the default state for the specified library.</returns>
        /// <remarks>The default library state will be returned as initialized.</remarks>
        private ILibraryState CreateDefaultLibraryState(IDynamicLibrary library)
        {
            IDynamicInitializableLibrary library2;
            IDefaultLibraryState libraryState;

            if (!(library is IDynamicInitializableLibrary))
                throw new LibraryStateException(LibraryMessages.Error058.FormatMessage(GetLibraryFileName(library), typeof(IDynamicInitializableLibrary).FullName));

            library2 = (IDynamicInitializableLibrary)library;

            libraryState = library2.CreateDefaultLibraryState();

            libraryState.Initialize();

            return(libraryState);
        }

        /// <summary>
        /// Creates an initialized library state for a dynamic custom XML configurable library based on an XML
        /// configuration file.
        /// </summary>
        /// <param name="library">Specifies a dynamic custom XML configurable library.</param>
        /// <param name="configFilePath">Specifies an XML configuration file path.</param>
        /// <returns>Returns the created and initialized <see cref="ILibraryState"/> object.</returns>
        private ILibraryState CreateLibraryState(IDynamicCustomXmlConfigurableLibrary library, string configFilePath)
        {
            XmlDocument config;
            ICustomXmlConfigurableLibraryState libraryState;

            config = LoadAndValidateXmlConfigFile(configFilePath, library.GetConfigSchemas(), GetLibraryFileName(library));

            libraryState = library.CreateCustomXmlConfigurableLibraryState();

            libraryState.Initialize(config);

            return(libraryState);
        }

        /// <summary>
        /// Creates an initialized library state for a dynamic library based on a configuration file.
        /// </summary>
        /// <param name="library">Specifies a dynamic library.</param>
        /// <param name="configFilePath">Specifies a configuration file path.</param>
        /// <returns>Returns the created and initialized <see cref="ILibraryState"/> object.</returns>
        private ILibraryState CreateLibraryState(IDynamicLibrary library, string configFilePath)
        {
            // Case dynamic custom XML configurable library
            if (library is IDynamicCustomXmlConfigurableLibrary)
                return(CreateLibraryState((IDynamicCustomXmlConfigurableLibrary)library, configFilePath));

            // Add other dynamic library types here
            // ...

            // Any of the required dynamic library interfaces wasn't found
            else
                throw new LibraryStateException(LibraryMessages.Error068.FormatMessage(GetLibraryFileName(library)));
        }

        /// <summary>
        /// Gets the file name of a library specified by a library interface.
        /// </summary>
        /// <param name="libraryInterface">Specifies a library interface object.</param>
        /// <returns>Returns the file name of the library specified by the given library interface object.</returns>
        private static string GetLibraryFileName(object libraryInterface)
        {
            return(((ILibraryHandle)libraryInterface).LibraryFileName);
        }

        /// <summary>
        /// Initializes a custom XML configurable library based on an XML configuration.
        /// </summary>
        /// <param name="library">Specifies a custom XML configurable library.</param>
        /// <param name="configFilePath">Specifies an XML configuration file path.</param>
        private void InitializeCustomXmlConfigurableLibrary(ICustomXmlConfigurableLibrary library, string configFilePath)
        {
            XmlDocument config;

            config = LoadAndValidateXmlConfigFile(configFilePath, library.GetConfigSchemas(), GetLibraryFileName(library));

            library.InitializeLibrary(config);
        }

        /// <summary>
        /// Initializes a dynamic custom XML configurable library based on an XML configuration.
        /// </summary>
        /// <param name="library">Specifies a dynamic custom XML configurable library.</param>
        /// <param name="configFilePath">Specifies an XML configuration file path.</param>
        private void InitializeDynamicCustomXmlConfigurableLibrary(IDynamicCustomXmlConfigurableLibrary library, string configFilePath)
        {
            library.LibraryState = CreateLibraryState(library, configFilePath);
        }

        /// <summary>
        /// Initializes the library specified by a library handle.
        /// </summary>
        /// <param name="libraryHandle">Specifies a library handle.</param>
        private void InitializeLibrary(ILibraryHandle libraryHandle)
        {
            bool requiresConfigFile;
            IConfigurableLibrary configurableLibrary;
            string configFilePath;
            List<IDynamicLibrary> dynamicLibraries;

            try
            {
                // Return immediately if the library is already initialized

                if (IsLibraryInitialized(libraryHandle))
                {
                    Logger.LogWarning(LibraryMessages.Warning023.FormatMessage(libraryHandle.LibraryFileName));

                    return;
                }

                // Push the library to the stack of library handles
                // Note: This is done before the initialization, because if the initialization process fails, the
                // closing process can clean up such initialization actions that were successfully accomplished prior
                // to the error in the initialization
                m_libraryHandles.Push(libraryHandle);

                // Initialize the library if necessary

                if (libraryHandle is IInitializableLibrary || libraryHandle is IConfigurableLibrary)
                {
                    // Check if the library requires a configuration file
                    requiresConfigFile = !(libraryHandle is IInitializableLibrary);

                    if (libraryHandle is IConfigurableLibrary)
                    {
                        // The library is configurable

                        configurableLibrary = (IConfigurableLibrary)libraryHandle;

                        configFilePath = Startup.ConfigDirectory + Path.DirectorySeparatorChar + configurableLibrary.ConfigFileName;

                        if (File.Exists(configFilePath))
                        {
                            // A configuration file exists for the library

                            // Initialize the library based on the configuration file

                            if (libraryHandle is IDynamicCustomXmlConfigurableLibrary)
                                InitializeDynamicCustomXmlConfigurableLibrary((IDynamicCustomXmlConfigurableLibrary)libraryHandle, configFilePath);

                            else if (libraryHandle is ICustomXmlConfigurableLibrary)
                                InitializeCustomXmlConfigurableLibrary((ICustomXmlConfigurableLibrary)libraryHandle, configFilePath);

                            // Add other configurable library types here
                            // ...

                            // Any of the required configurable library interfaces wasn't found
                            else
                                throw new LibraryInitializationException(LibraryMessages.Error069.FormatMessage(libraryHandle.LibraryFileName, configFilePath));
                        }

                        else if (requiresConfigFile)
                            // The library requires a configuration file but such doesn't exist
                            throw new ConfigException(LibraryMessages.Error001.FormatMessage(libraryHandle.LibraryFileName, configurableLibrary.ConfigFileName, Startup.ConfigDirectory));

                        else
                            // There is no configuration file but the library is also initializable, so just initialize it
                            ((IInitializableLibrary)libraryHandle).InitializeLibrary();

                        // Add the library to the list of dynamic libraries if necessary

                        if (libraryHandle is IDynamicLibrary)
                        {
                            if (!m_dynamicLibraries.TryGetValue(configurableLibrary.ConfigFileName, out dynamicLibraries))
                            {
                                dynamicLibraries = new List<IDynamicLibrary>();

                                m_dynamicLibraries.Add(configurableLibrary.ConfigFileName, dynamicLibraries);
                            }

                            dynamicLibraries.Add((IDynamicLibrary)libraryHandle);
                        }
                    }
                    else
                        // The library is only initializable, initialize it
                        ((IInitializableLibrary)libraryHandle).InitializeLibrary();
                }

                // Start the library processes if necessary

                if (libraryHandle is IDynamicLibrary)
                    StartLibraryStateProcesses(((IDynamicLibrary)libraryHandle).LibraryState);
                else
                    StartLibraryProcesses(libraryHandle);
            }

            catch (Exception ex)
            {
                throw new LibraryInitializationException(LibraryMessages.Error003.FormatMessage(libraryHandle.LibraryFileName), ex);
            }
        }

        /// <summary>
        /// Checks whether the library specified by a library handle is already initialized.
        /// </summary>
        /// <param name="libraryHandle">Specifies a library handle.</param>
        /// <returns>Returns true if the library specified by the library handle is already initialized, otherwise
        /// false.</returns>
        private bool IsLibraryInitialized(ILibraryHandle libraryHandle)
        {
            string libraryFileName = libraryHandle.LibraryFileName.ToLower();

            foreach (ILibraryHandle libraryHandle2 in m_libraryHandles)
                if (libraryHandle2.LibraryFileName.ToLower() == libraryFileName)
                    return(true);

            return(false);
        }

        /// <summary>
        /// Loads and validates an XML configuration file againts XML schemas.
        /// </summary>
        /// <param name="configFilePath">Specifies an XML configuration file path.</param>
        /// <param name="configSchemas">Specifies an array of XML schemas. Can be null in which case the validation
        /// will be ignored.</param>
        /// <param name="libraryFileName">Specifies the file name of a library to which the XML schemas relate.</param>
        /// <returns>Returns an <see cref="XmlDocument"/> object containing the validated XML configuration.</returns>
        private static XmlDocument LoadAndValidateXmlConfigFile(string configFilePath, XmlSchema[] configSchemas, string libraryFileName)
        {
            XmlDocument config;
            XmlValidator validator;

            config = new XmlDocument();

            config.Load(configFilePath);

            if (configSchemas == null)
                return(config);

            try
            {
                validator = new XmlValidator();

                foreach (XmlSchema schema in configSchemas)
                    validator.AddSchema(schema);

                validator.Validate(config);

                return(config);
            }

            catch (XmlSchemaValidationException ex)
            {
                throw new InvalidConfigFileException(LibraryMessages.Error002.FormatMessage(configFilePath, libraryFileName), ex);
            }
        }

        /// <summary>
        /// Defines a event handler that will be fired when a configuration file is changed or created.
        /// </summary>
        /// <param name="source">Specifies an event source.</param>
        /// <param name="e">Specifies event arguments.</param>
        private void OnConfigFileCreatedOrChanged(object source, FileSystemEventArgs e)
        {
            List<IDynamicLibrary> dynamicLibraries;
            ILibraryState libraryState;

            try
            {
                // Get the dynamic libraries associated with the created or changed configuration file

                if (!m_dynamicLibraries.TryGetValue(e.Name, out dynamicLibraries))
                {
                    Logger.LogWarning(LibraryMessages.Warning063, e.FullPath);

                    return;
                }

                // Update the states of the associated dynamic libraries

                foreach (IDynamicLibrary library in dynamicLibraries)
                {
                    try
                    {
                        libraryState = CreateLibraryState(library, e.FullPath);
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError(LibraryMessages.Error064, e.FullPath, GetLibraryFileName(library), ex);

                        continue;
                    }

                    try
                    {
                        UpdateLibraryState(library, libraryState);

                        Logger.LogInformation(LibraryMessages.Information065, e.FullPath, GetLibraryFileName(library));
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError(LibraryMessages.Error066, e.FullPath, GetLibraryFileName(library), ex);
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(LibraryMessages.Error067, e.FullPath, ex);
            }
        }

        /// <summary>
        /// Defines a event handler that will be fired when a configuration file is deleted.
        /// </summary>
        /// <param name="source">Specifies an event source.</param>
        /// <param name="e">Specifies event arguments.</param>
        private void OnConfigFileDeleted(object source, FileSystemEventArgs e)
        {
            List<IDynamicLibrary> dynamicLibraries;
            ILibraryState libraryState;

            try
            {
                // Get the dynamic libraries associated with the deleted configuration file

                if (!m_dynamicLibraries.TryGetValue(e.Name, out dynamicLibraries))
                {
                    Logger.LogWarning(LibraryMessages.Warning011, e.FullPath);

                    return;
                }

                // Initialize the associated dynamic libraries

                foreach (IDynamicLibrary library in dynamicLibraries)
                {
                    try
                    {
                        libraryState = CreateDefaultLibraryState(library);
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError(LibraryMessages.Error056, e.FullPath, GetLibraryFileName(library), ex);

                        continue;
                    }

                    try
                    {
                        UpdateLibraryState(library, libraryState);

                        Logger.LogInformation(LibraryMessages.Information012, e.FullPath, GetLibraryFileName(library));
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError(LibraryMessages.Error057, e.FullPath, GetLibraryFileName(library), ex);
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(LibraryMessages.Error013, e.FullPath, ex);
            }
        }

        /// <summary>
        /// Starts the processes of a specified non-dynamic library if necessary.
        /// </summary>
        /// <param name="libraryHandle">Specifies a library handle.</param>
        private void StartLibraryProcesses(ILibraryHandle libraryHandle)
        {
            IStartableLibrary startableLibrary = libraryHandle as IStartableLibrary;

            if (startableLibrary == null)
                // There is nothing to start
                return;

            try
            {
                startableLibrary.StartProcesses();
            }

            catch (Exception ex)
            {
                throw new LibraryInitializationException(LibraryMessages.Error070.FormatMessage(libraryHandle.LibraryFileName), ex);
            }
        }

        /// <summary>
        /// Starts the processes in a library state if necessary.
        /// </summary>
        /// <param name="libraryState">Specifies a library state.</param>
        private void StartLibraryStateProcesses(ILibraryState libraryState)
        {
            IStartableLibraryState startableLibraryState = libraryState as IStartableLibraryState;

            if (startableLibraryState == null)
                // There is nothing to start
                return;

            try
            {
                startableLibraryState.StartProcesses();
            }

            catch (Exception ex)
            {
                throw new LibraryStateException(LibraryMessages.Error061.FormatMessage(GetLibraryFileName(libraryState.LibraryHandle), ex));
            }
        }

        /// <summary>
        /// Stops the processes in a library state if necessary.
        /// </summary>
        /// <param name="libraryState">Specifies a library state. Can be null.</param>
        /// <param name="dynamicOperation">Specifies whether the operation is dynamic.</param>
        private void StopLibraryStateProcesses(ILibraryState libraryState, bool dynamicOperation)
        {
            IStartableLibraryState startableLibraryState;

            if (libraryState == null)
                return;

            startableLibraryState = libraryState as IStartableLibraryState;

            if (startableLibraryState == null)
                // There is nothing to stop
                return;

            try
            {
                if (!startableLibraryState.StopProcesses())
                    if (dynamicOperation)
                        throw new LibraryStateException(LibraryMessages.Error059.FormatMessage(GetLibraryFileName(libraryState.LibraryHandle)));
                    else
                        Logger.LogWarning(LibraryMessages.Warning073.FormatMessage(GetLibraryFileName(libraryState.LibraryHandle)));
            }

            catch (Exception ex)
            {
                if (dynamicOperation)
                    throw new LibraryStateException(LibraryMessages.Error075.FormatMessage(GetLibraryFileName(libraryState.LibraryHandle), ex));
                else
                    Logger.LogError(LibraryMessages.Error075.FormatMessage(GetLibraryFileName(libraryState.LibraryHandle), ex));
            }
        }

        /// <summary>
        /// Updates the state of a specified dynamic library.
        /// </summary>
        /// <param name="library">Specifies a dynamic library.</param>
        /// <param name="newLibraryState">Specifies a new library state.</param>
        private void UpdateLibraryState(IDynamicLibrary library, ILibraryState newLibraryState)
        {
            ILibraryState currentLibraryState;

            // Acquire a write lock to the library state
            library.LibraryStateLock.EnterWriteLock();

            try
            {
                currentLibraryState = library.LibraryState;

                try
                {
                    // Try to stop the processes in the current library state if necessary
                    StopLibraryStateProcesses(currentLibraryState, true);
                }

                catch
                {
                    // Failed to stop the processes in the current library state

                    // Close the new state
                    CloseLibraryState(currentLibraryState);

                    // Rethrow the exception
                    throw;
                }

                // Try to start the processes in the new library state if necessary

                try
                {
                    StartLibraryStateProcesses(newLibraryState);
                }

                catch (Exception ex)
                {
                    // Try to roll back what was possibly accomplished

                    //TryToRestoreCurrentState(currentLibraryState, newLibraryState);

                    try
                    {
                        ((IDynamicStartableLibrary)library).StopProcesses(newLibraryState);
                    }

                    catch (Exception ex2)
                    {
                        Logger.LogError(LibraryMessages.Error074.FormatMessage(GetLibraryFileName(library), ex2));
                    }

                    throw new LibraryStateException(LibraryMessages.Error061.FormatMessage(GetLibraryFileName(library), ex));
                }

                // Finally, set the new library state

                try
                {
                    library.LibraryState = newLibraryState;
                }

                catch (Exception ex)
                {
                    throw new LibraryStateException(LibraryMessages.Error062.FormatMessage(GetLibraryFileName(library), ex));
                }
            }

            finally
            {
                // Release the write lock to the library state
                library.LibraryStateLock.ExitWriteLock();
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// A ConfigFileWatcher object that watches configuration file changes in the configuration directory.
        /// </summary>
        private ConfigFileWatcher m_configFileWatcher;

        /// <summary>
        /// Specifies the collection of the dynamic libraries with configuration file names as key values.
        /// </summary>
        private Dictionary<string, List<IDynamicLibrary>> m_dynamicLibraries;

        /// <summary>
        /// Specifies the stack of the initialized libraries.
        /// </summary>
        private Stack<ILibraryHandle> m_libraryHandles;

        #endregion
    }
}
