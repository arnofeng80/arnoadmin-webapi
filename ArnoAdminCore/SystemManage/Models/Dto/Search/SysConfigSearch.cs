using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto.Search
{
    public class SysConfigSearch : BasePageSearch
    {
        public String ConfigName { get; set; }
        public String ConfigKey { get; set; }
        public String ConfigType { get; set; }
        public DateRange CreateTime { get; set; }
    }
}
