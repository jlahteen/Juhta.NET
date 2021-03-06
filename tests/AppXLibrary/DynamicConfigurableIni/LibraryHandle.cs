﻿
using Juhta.Net.LibraryManagement;

namespace AppXLibrary.DynamicConfigurableIni
{
    public class LibraryHandle : DynamicLibraryHandleBase, IDynamicConfigurableLibrary
    {
        #region Public Constructors

        public LibraryHandle() : base("AppXLibrary.dll")
        {}

        #endregion

        #region Public Methods

        public IConfigurableLibraryState CreateLibraryState()
        {
            return(new LibraryState());
        }

        #endregion

        #region Public Properties

        public override string ConfigFileName
        {
            get {return("AppXLibrary.ini");}
        }

        #endregion
    }
}
