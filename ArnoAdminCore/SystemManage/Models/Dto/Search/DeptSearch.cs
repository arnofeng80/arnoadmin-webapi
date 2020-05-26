using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto.Search
{
    public class DeptSearch : BasePageSearch
    {
        public String DeptName { get; set; }
        public String Status { get; set; }
    }
}
