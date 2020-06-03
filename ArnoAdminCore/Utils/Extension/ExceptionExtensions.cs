using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Utils.Extension
{
    public static class ExceptionExtensions
    {
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }
    }
}
