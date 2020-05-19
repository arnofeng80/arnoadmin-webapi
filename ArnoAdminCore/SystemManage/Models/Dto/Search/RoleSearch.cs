using ArnoAdminCore.Base.Models;
using System;

namespace ArnoAdminCore.SystemManage.Models.Dto.Search
{
    public class RoleSearch : BasePageSearch
    {
        public String RoleName { get; set; }
        public String RoleCode { get; set; }
        public String Status { get; set; }
        public DateRange CreateTime { get; set; }
    }
}
