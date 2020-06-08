using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Web
{
    public class Operator
    {
        public long Id { get; set; }
        public long DeptId { get; set; }
        public String LoginName { get; set; }
        public String NameChn { get; set; }
        public String NameEng { get; set; }
        public String UserType { get; set; }
        public String Email { get; set; }
        public String InternalPhone { get; set; }
        public String Mobile { get; set; }
        public String Gender { get; set; }

        public List<Menu> Menus { get; set; }
        public List<Role> Roles { get; set; }

    }
}
