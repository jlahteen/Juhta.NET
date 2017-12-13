
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;

namespace Juhta.Net
{
    /// <summary>
    /// A static class that defines the diagnostic messages for this library.
    /// </summary>
    internal static class LibraryMessages
    {
        #region Private Properties

        /// <summary>
        /// Gets the DiagnosticMessageFactory instance for creating diagnostic messages.
        /// </summary>
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(DiagnosticMessageIdBase.RootLibraryMessages, typeof(LibraryMessages).Namespace);

        #endregion

        #region Internal Properties

        /// <summary>
        /// Library '{0}' requires a configuration file '{1}' but there is no such file in the configuration directory
        /// '{2}'.
        /// </summary>
        internal static readonly ErrorMessage Error001 = MessageFactory.CreateErrorMessage("Library '{0}' requires a configuration file '{1}' but there is no such file in the configuration directory '{2}'.");

        /// <summary>
        /// XML configuration file '{0}' does not conform to the configuration schema(s) of the custom XML configurable
        /// library '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error002 = MessageFactory.CreateErrorMessage("XML configuration file '{0}' does not conform to the configuration schema(s) of the custom XML configurable library '{1}'.");

        /// <summary>
        /// Initialization of the library '{0}' failed.
        /// </summary>
        internal static readonly ErrorMessage Error003 = MessageFactory.CreateErrorMessage("Initialization of the library '{0}' failed.");

        /// <summary>
        /// An unexpected error occurred when the library '{0}' was being closed.
        /// </summary>
        internal static readonly ErrorMessage Error004 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the library '{0}' was being closed.");

        /// <summary>
        /// Library Manager detected changes in the configuration but failed to update the states of the associated
        /// dynamic libraries. The state of the process may be unstable. Please refer to the log events for more
        /// information.
        /// </summary>
        internal static readonly AlertMessage Alert005 = MessageFactory.CreateAlertMessage("Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information.");

        /// <summary>
        /// An error occurred when the application '{0}' was being started.
        /// </summary>
        internal static readonly ErrorMessage Error006 = MessageFactory.CreateErrorMessage("An error occurred when the application '{0}' was being started.");

        /// <summary>
        /// Application '{0}' failed to start. Please refer to the log events for more information.
        /// </summary>
        internal static readonly AlertMessage Alert007 = MessageFactory.CreateAlertMessage("Application '{0}' failed to start. Please refer to the log events for more information.");

        /// <summary>
        /// Dynamic library context could not be created because no dynamic library was found with the type '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error008 = MessageFactory.CreateErrorMessage("Dynamic library context could not be created because no dynamic library was found with the type '{0}'.");

        // NOTE: Message ID 9 is free

        /// <summary>
        /// Free for the future use.
        /// </summary>
        internal static readonly ErrorMessage Error009 = MessageFactory.CreateErrorMessage("Free for the future use.");

        // NOTE: Message ID 10 is free

        /// <summary>
        /// Free for the future use.
        /// </summary>
        internal static readonly ErrorMessage Error010 = MessageFactory.CreateErrorMessage("Free for the future use.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but no actions were performed
        /// because there were no dynamic libraries associated with this configuration file.
        /// </summary>
        internal static readonly WarningMessage Warning011 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was deleted, but no actions were performed because there were no dynamic libraries associated with this configuration file.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, and the state of the associated
        /// dynamic library '{1}' was initialized successfully.
        /// </summary>
        internal static readonly InformationMessage Information012 = MessageFactory.CreateInformationMessage("Library Manager detected that the configuration file '{0}' was deleted, and the state of the associated dynamic library '{1}' was initialized successfully.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but an unexpected error occurred
        /// when the states of the associated dynamic libraries were being initialized. NOTE: The state of the process
        /// is currently unstable. You should restore the configuration file and possibly restart the process.
        /// </summary>
        internal static readonly ErrorMessage Error013 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was deleted, but an unexpected error occurred when the states of the associated dynamic libraries were being initialized. NOTE: The state of the process is currently unstable. You should restore the configuration file and possibly restart the process.");

        // NOTE: Message ID 14 is free

        /// <summary>
        /// Free for the future use.
        /// </summary>
        internal static readonly ErrorMessage Error014 = MessageFactory.CreateErrorMessage("Free for the future use.");

        // NOTE: Message ID 15 is free

        /// <summary>
        /// Free for the future use.
        /// </summary>
        internal static readonly ErrorMessage Error015 = MessageFactory.CreateErrorMessage("Free for the future use.");

        // NOTE: Message ID 16 is free

        /// <summary>
        /// Free for the future use.
        /// </summary>
        internal static readonly ErrorMessage Error016 = MessageFactory.CreateErrorMessage("Free for the future use.");

        /// <summary>
        /// At least one error occurred when the library '{0}' was closed. All resources and services of this library
        /// may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning017 = MessageFactory.CreateWarningMessage("At least one error occurred when the library '{0}' was closed. All resources and services of this library may not have been completely released or shutted down.");

        /// <summary>
        /// An unexpected error occurred when the application '{0}' was being closed.
        /// </summary>
        internal static readonly ErrorMessage Error018 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the application '{0}' was being closed.");

        /// <summary>
        /// An error occurred when a pending configuration file event was being created for the configuration file
        /// '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error019 = MessageFactory.CreateErrorMessage("An error occurred when a pending configuration file event was being created for the configuration file '{0}'.");

        /// <summary>
        /// Dynamic configuration changes related to the configuration file '{0}' will probably not come into effect.
        /// </summary>
        internal static readonly WarningMessage Warning020 = MessageFactory.CreateWarningMessage("Dynamic configuration changes related to the configuration file '{0}' will probably not come into effect.");

        /// <summary>
        /// An error occurred when a pending configuration file event related to the configuration file '{0}' was being
        /// raised.
        /// </summary>
        internal static readonly ErrorMessage Error021 = MessageFactory.CreateErrorMessage("An error occurred when a pending configuration file event related to the configuration file '{0}' was being raised.");

        /// <summary>
        /// Library '{0}' does not exist in the directory '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error022 = MessageFactory.CreateErrorMessage("Library '{0}' does not exist in the directory '{1}'.");

        /// <summary>
        /// Library '{0}' has already been initialized. This library exists at least twice under the libraries XML node
        /// in the root library configuration. Please remove duplicate occurrences.
        /// </summary>
        internal static readonly WarningMessage Warning023 = MessageFactory.CreateWarningMessage("Library '{0}' has already been initialized. This library exists at least twice under the libraries XML node in the root library configuration. Please remove duplicate occurrences.");

        /// <summary>
        /// Local assembly path has already been associated with this AssemblyClassUri instance.
        /// </summary>
        internal static readonly ErrorMessage Error024 = MessageFactory.CreateErrorMessage("Local assembly path has already been associated with this AssemblyClassUri instance.");

        /// <summary>
        /// Value '{0}' is not a valid assembly class file URI.
        /// </summary>
        internal static readonly ErrorMessage Error025 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid assembly class file URI.");

        /// <summary>
        /// Assembly specified by the URI '{0}' has not been downloaded to the local machine.
        /// </summary>
        internal static readonly ErrorMessage Error026 = MessageFactory.CreateErrorMessage("Assembly specified by the URI '{0}' has not been downloaded to the local machine.");

        /// <summary>
        /// Consumer thread cannot consume the specified object because the instance has not been started.
        /// </summary>
        internal static readonly ErrorMessage Error027 = MessageFactory.CreateErrorMessage("Consumer thread cannot consume the specified object because the instance has not been started.");

        /// <summary>
        /// Consumer thread cannot consume the specified object because the internal worker thread has stopped due to
        /// an unexpected error.
        /// </summary>
        internal static readonly ErrorMessage Error028 = MessageFactory.CreateErrorMessage("Consumer thread cannot consume the specified object because the internal worker thread has stopped due to an unexpected error.");

        /// <summary>
        /// Local assembly path to be associated with an AssemblyClassUri instance must refer to an assembly on a fixed
        /// disk drive.
        /// </summary>
        internal static readonly ErrorMessage Error029 = MessageFactory.CreateErrorMessage("Local assembly path to be associated with an AssemblyClassUri instance must refer to an assembly on a fixed disk drive.");

        /// <summary>
        /// Object cannot be created based on the URI '{0}' because the referenced assembly has not been downloaded to
        /// the local machine.
        /// </summary>
        internal static readonly ErrorMessage Error030 = MessageFactory.CreateErrorMessage("Object cannot be created based on the URI '{0}' because the referenced assembly has not been downloaded to the local machine.");

        /// <summary>
        /// Execution flow of the software reached a code branch that should never be reached (hint: {0}).
        /// </summary>
        internal static readonly ErrorMessage Error031 = MessageFactory.CreateErrorMessage("Execution flow of the software reached a code branch that should never be reached (hint: {0}).");

        /// <summary>
        /// XML document cannot be validated because the target namespace '{0}' is not present in the schema collection
        /// of the XML validator.
        /// </summary>
        internal static readonly ErrorMessage Error032 = MessageFactory.CreateErrorMessage("XML document cannot be validated because the target namespace '{0}' is not present in the schema collection of the XML validator.");

        /// <summary>
        /// Name part cannot be empty in a command line option ('{0}'). The common syntax of command line options is
        /// &lt;prefix&gt;&lt;name&gt;[&lt;nameValueSeparator&gt;&lt;value&gt;].
        /// </summary>
        internal static readonly ErrorMessage Error033 = MessageFactory.CreateErrorMessage("Name part cannot be empty in a command line option ('{0}'). The common syntax of command line options is <prefix><name>[<nameValueSeparator><value>].");

        /// <summary>
        /// Value part cannot be empty in a command line option ('{0}'). The common syntax of command line options is
        /// &lt;prefix&gt;&lt;name&gt;[&lt;nameValueSeparator&gt;&lt;value&gt;].
        /// </summary>
        internal static readonly ErrorMessage Error034 = MessageFactory.CreateErrorMessage("Value part cannot be empty in a command line option ('{0}'). The common syntax of command line options is <prefix><name>[<nameValueSeparator><value>].");

        /// <summary>
        /// More than one mutually exclusive options from the option group '{0}' were found in the command line
        /// arguments.
        /// </summary>
        internal static readonly ErrorMessage Error035 = MessageFactory.CreateErrorMessage("More than one mutually exclusive options from the option group '{0}' were found in the command line arguments.");

        /// <summary>
        /// None of the options from the option group '{0}' were found in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error036 = MessageFactory.CreateErrorMessage("None of the options from the option group '{0}' were found in the command line arguments.");

        /// <summary>
        /// Prefix '{0}' for command line options is not valid according to the regular expression pattern '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error037 = MessageFactory.CreateErrorMessage("Prefix '{0}' for command line options is not valid according to the regular expression pattern '{1}'.");

        /// <summary>
        /// Separator '{0}' for name and value parts in command line options is not valid according to the regular
        /// expression pattern '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error038 = MessageFactory.CreateErrorMessage("Separator '{0}' for name and value parts in command line options is not valid according to the regular expression pattern '{1}'.");

        /// <summary>
        /// Command line option name '{0}' is not valid according to the regular expression pattern '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error039 = MessageFactory.CreateErrorMessage("Command line option name '{0}' is not valid according to the regular expression pattern '{1}'.");

        /// <summary>
        /// Default option's name was not found in the option group '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error040 = MessageFactory.CreateErrorMessage("Default option's name was not found in the option group '{0}'.");

        /// <summary>
        /// Default option's name must be '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error041 = MessageFactory.CreateErrorMessage("Default option's name must be '{0}'.");

        /// <summary>
        /// Option '{0}' was specified more than once in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error042 = MessageFactory.CreateErrorMessage("Option '{0}' was specified more than once in the command line arguments.");

        /// <summary>
        /// Option '{0}' was not found in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error043 = MessageFactory.CreateErrorMessage("Option '{0}' was not found in the command line arguments.");

        /// <summary>
        /// Command line argument '{0}' has no value part.
        /// </summary>
        internal static readonly ErrorMessage Error044 = MessageFactory.CreateErrorMessage("Command line argument '{0}' has no value part.");

        /// <summary>
        /// Command line argument '{0}' is invalid according to the given validator.
        /// </summary>
        internal static readonly ErrorMessage Error045 = MessageFactory.CreateErrorMessage("Command line argument '{0}' is invalid according to the given validator.");

        /// <summary>
        /// Command line argument '{0}' is invalid according to the regular expression '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error046 = MessageFactory.CreateErrorMessage("Command line argument '{0}' is invalid according to the regular expression '{1}'.");

        /// <summary>
        /// Command line argument value '{0}' is invalid according to the given validator.
        /// </summary>
        internal static readonly ErrorMessage Error047 = MessageFactory.CreateErrorMessage("Command line argument value '{0}' is invalid according to the given validator.");

        /// <summary>
        /// Command line argument value '{0}' is invalid according to the regular expression '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error048 = MessageFactory.CreateErrorMessage("Command line argument value '{0}' is invalid according to the regular expression '{1}'.");

        /// <summary>
        /// Command line argument '{0}' is unexpected.
        /// </summary>
        internal static readonly ErrorMessage Error049 = MessageFactory.CreateErrorMessage("Command line argument '{0}' is unexpected.");

        /// <summary>
        /// There are no command line parameters to be consumed.
        /// </summary>
        internal static readonly ErrorMessage Error050 = MessageFactory.CreateErrorMessage("There are no command line parameters to be consumed.");

        /// <summary>
        /// Regular expression pattern cannot be null.
        /// </summary>
        internal static readonly ErrorMessage Error051 = MessageFactory.CreateErrorMessage("Regular expression pattern cannot be null.");

        /// <summary>
        /// Value '{0}' is not a valid finnish social security number.
        /// </summary>
        internal static readonly ErrorMessage Error052 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid finnish social security number.");

        /// <summary>
        /// Value '{0}' is invalid according to the regular expression pattern '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error053 = MessageFactory.CreateErrorMessage("Value '{0}' is invalid according to the regular expression pattern '{1}'.");

        /// <summary>
        /// File '{0}' could not be locked within {1} milliseconds.
        /// </summary>
        internal static readonly ErrorMessage Error054 = MessageFactory.CreateErrorMessage("File '{0}' could not be locked within {1} milliseconds.");

        /// <summary>
        /// Range between positions {0} - {1} in the file '{2}' could not be locked within {3} milliseconds.
        /// </summary>
        internal static readonly ErrorMessage Error055 = MessageFactory.CreateErrorMessage("Range between positions {0} - {1} in the file '{2}' could not be locked within {3} milliseconds.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated
        /// dynamic library '{1}' could not be initialized. The state of the library was left unmodified.
        /// </summary>
        internal static readonly ErrorMessage Error056 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated dynamic library '{1}' could not be initialized. The state of the library was left unmodified.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated
        /// dynamic library '{1}' could not be initialized. NOTE: The state of the library is currently unstable. You
        /// should restore the configuration file and possibly restart the process.
        /// </summary>
        internal static readonly ErrorMessage Error057 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated dynamic library '{1}' could not be initialized. NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.");

        /// <summary>
        /// Default library state could not be created because the library '{0}' does not support the required
        /// interface ('{1}').
        /// </summary>
        internal static readonly ErrorMessage Error058 = MessageFactory.CreateErrorMessage("Default library state could not be created because the library '{0}' does not support the required interface ('{1}').");

        /// <summary>
        /// Processes in the {0} state of the library '{1}' could not be completely stopped.
        /// </summary>
        internal static readonly ErrorMessage Error059 = MessageFactory.CreateErrorMessage("Processes in the {0} state of the library '{1}' could not be completely stopped.");

        // NOTE: Message ID 60 is free

        /// <summary>
        /// Free for the future use.
        /// </summary>
        internal static readonly ErrorMessage Error060 = MessageFactory.CreateErrorMessage("Free for the future use.");

        /// <summary>
        /// Processes in the {0} state of the library '{1}' could not be started.
        /// </summary>
        internal static readonly ErrorMessage Error061 = MessageFactory.CreateErrorMessage("Processes in the {0} state of the library '{1}' could not be started.");

        // NOTE: Message ID 62 is free

        /// <summary>
        /// Free for the future use.
        /// </summary>
        internal static readonly ErrorMessage Error062 = MessageFactory.CreateErrorMessage("Free for the future use.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but no actions were
        /// performed because there were no dynamic libraries associated with this configuration file.
        /// </summary>
        internal static readonly WarningMessage Warning063 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was created or changed, but no actions were performed because there were no dynamic libraries associated with this configuration file.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but the state of the
        /// associated dynamic library '{1}' could not be updated. The state of the library was left unmodified.
        /// </summary>
        internal static readonly ErrorMessage Error064 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was created or changed, but the state of the associated dynamic library '{1}' could not be updated. The state of the library was left unmodified.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, and the state of the
        /// associated dynamic library '{1}' was updated successfully.
        /// </summary>
        internal static readonly InformationMessage Information065 = MessageFactory.CreateInformationMessage("Library Manager detected that the configuration file '{0}' was created or changed, and the state of the associated dynamic library '{1}' was updated successfully.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but the state of the
        /// associated dynamic library '{1}' could not be updated. NOTE: The state of the library is currently
        /// unstable. You should restore the configuration file and possibly restart the process.
        /// </summary>
        internal static readonly ErrorMessage Error066 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was created or changed, but the state of the associated dynamic library '{1}' could not be updated. NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but an unexpected error
        /// occurred when the states of the associated dynamic libraries were being updated. NOTE: The state of the
        /// process is currently unstable. You should restore the configuration file and possibly restart the process.
        /// </summary>
        internal static readonly ErrorMessage Error067 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was created or changed, but an unexpected error occurred when the states of the associated dynamic libraries were being updated. NOTE: The state of the process is currently unstable. You should restore the configuration file and possibly restart the process.");

        /// <summary>
        /// Library state cannot be created for the library '{0}', because the library doesn't implement any of the
        /// required dynamic library interfaces.
        /// </summary>
        internal static readonly ErrorMessage Error068 = MessageFactory.CreateErrorMessage("Library state cannot be created for the library '{0}', because the library doesn't implement any of the required dynamic library interfaces.");

        /// <summary>
        /// Library '{0}' cannot be initialized based on the configuration file '{1}', because the library doesn't
        /// implement any of the required configurable library interfaces.
        /// </summary>
        internal static readonly ErrorMessage Error069 = MessageFactory.CreateErrorMessage("Library '{0}' cannot be initialized based on the configuration file '{1}', because the library doesn't implement any of the required configurable library interfaces.");

        /// <summary>
        /// Processes of the library '{0}' could not be started.
        /// </summary>
        internal static readonly ErrorMessage Error070 = MessageFactory.CreateErrorMessage("Processes of the library '{0}' could not be started.");

        /// <summary>
        /// An unexpected error occurred when the processes of the library '{0}' were being stopped.
        /// </summary>
        internal static readonly ErrorMessage Error071 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the processes of the library '{0}' were being stopped.");

        /// <summary>
        /// At least one error occurred when the processes of the library '{0}' were being stopped. All resources and
        /// services of these processes may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning072 = MessageFactory.CreateWarningMessage("At least one error occurred when the processes of the library '{0}' were being stopped. All resources and services of these processes may not have been completely released or shutted down.");

        /// <summary>
        /// At least one error occurred when the processes in the {0} state of the library '{1}' were being stopped.
        /// All resources and services of these processes may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning073 = MessageFactory.CreateWarningMessage("At least one error occurred when the processes in the {0} state of the library '{1}' were being stopped. All resources and services of these processes may not have been completely released or shutted down.");

        // NOTE: Message ID 74 is free

        /// <summary>
        /// Free for the future use.
        /// </summary>
        internal static readonly ErrorMessage Error074 = MessageFactory.CreateErrorMessage("Free for the future use.");

        /// <summary>
        /// An unexpected error occurred when the processes in the {0} state of the library '{1}' were being stopped.
        /// </summary>
        internal static readonly ErrorMessage Error075 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the processes in the {0} state of the library '{1}' were being stopped.");

        /// <summary>
        /// At least one error occurred when the {0} state of the library '{1}' was closed. All resources and services
        /// of this library may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning076 = MessageFactory.CreateWarningMessage("At least one error occurred when the {0} state of the library '{1}' was closed. All resources and services of this library may not have been completely released or shutted down.");

        /// <summary>
        /// An unexpected error occurred when the {0} state of the library '{1}' was being closed.
        /// </summary>
        internal static readonly ErrorMessage Error077 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the {0} state of the library '{1}' was being closed.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed but the state of the
        /// associated dynamic library '{1}' could not be updated. NOTE: The library continues running with the current
        /// state.
        /// </summary>
        internal static readonly WarningMessage Warning078 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was created or changed but the state of the associated dynamic library '{1}' could not be updated. NOTE: The library continues running with the current state.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted but the state of the associated
        /// dynamic library '{1}' could not be initialized. NOTE: The library continues running with the current state.
        /// </summary>
        internal static readonly WarningMessage Warning079 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was deleted but the state of the associated dynamic library '{1}' could not be initialized. NOTE: The library continues running with the current state.");

        #endregion
    }
}