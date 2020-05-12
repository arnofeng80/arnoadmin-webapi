using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoCore.Models.SystemManage
{
    public class Department : BaseEntity
    {
        public long ParentId { get; set; }
        public String DeptName { get; set; }
        public int OrderNum { get; set; }
        public String Leader { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public int Status { get; set; }

    }
}
