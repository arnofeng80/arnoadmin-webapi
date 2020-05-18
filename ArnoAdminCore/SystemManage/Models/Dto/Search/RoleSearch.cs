using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto.Search
{
    public class RoleSearch : BasePageSearch
    {
        public String roleName { get; set; }
        public String roleCode { get; set; }
        public String Status { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
