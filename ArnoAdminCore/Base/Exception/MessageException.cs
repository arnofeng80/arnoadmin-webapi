using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base.Exception
{
    public class MessageException : System.Exception
    {
        public MessageException(string message) : base(message)
        {
        }
    }
}
