﻿
using Juhta.Net;

namespace AppXLibrary.DynamicCustomXmlConfigurableAndStartable
{
    public static class ReplaceProcessInfo
    {
        #region Public Properties

        public static bool IsStarted
        {
            get
            {
                using (var context = Application.Instance.CreateDynamicLibraryContext<LibraryHandle, LibraryState>())
                {
                    return(context.LibraryState.ReplaceProcess.IsStarted);
                }
            }
        }

        #endregion
    }
}
