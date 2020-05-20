using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class UserRole
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
