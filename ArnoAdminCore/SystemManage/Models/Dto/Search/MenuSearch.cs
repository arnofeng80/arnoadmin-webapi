using ArnoAdminCore.Base.Models;
using System;

namespace ArnoAdminCore.SystemManage.Models.Dto.Search
{
    public class MenuSearch : BasePageSearch
    {
        public String MenuName { get; set; }
        public String Status { get; set; }
    }
}
