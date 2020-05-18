using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class RoleDept
    {
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
        public long DeptId { get; set; }
        public virtual Department Department { get; set; }
    }
}
