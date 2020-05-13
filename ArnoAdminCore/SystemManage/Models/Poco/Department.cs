using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class Department : BaseEntity
    {
        public long ParentId { get; set; }
        public String DeptName { get; set; }
        public int OrderNum { get; set; }
        public String Leader { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Status { get; set; }
        //public List<User> Users { get; set; }
    }
}
