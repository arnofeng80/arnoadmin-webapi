using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class RoleMenu
    {
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
        public long MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
