﻿
using Juhta.Net.LibraryManagement;

namespace AppXLibrary
{
    public class InitializableLibrary : AppLibraryHandle, IInitializableLibrary
    {
        #region Public Constructors

        public InitializableLibrary() : base("AppXLibrary.dll")
        {}

        #endregion

        #region Public Methods

        public void InitializeLibrary()
        {
            LibraryConfig.IntSetting = 89473537;

            LibraryConfig.StringSetting = "This is the default value for the StringSetting.";
        }

        #endregion
    }
}
