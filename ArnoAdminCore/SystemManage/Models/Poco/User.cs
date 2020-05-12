using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class User : BaseEntity
    {
        public long DeptId { get; set; }
        public String LoginName { get; set; }
        public String NameChn { get; set; }
        public String NameEng { get; set; }
        public String UserType { get; set; }
        public String Email { get; set; }
        public String InternalPhone { get; set; }
        public String Mobile { get; set; }
        public String Gender { get; set; }
        public String Password { get; set; }
        public int Status { get; set; }
        public String LoginIp { get; set; }
        public DateTime LoginDate { get; set; }
        public String Remark { get; set; }
        //public Department Department { get; set; }
    }
}
