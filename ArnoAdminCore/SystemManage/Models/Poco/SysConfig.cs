using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class SysConfig : BaseEntity
    {
        public String ConfigName { get; set; }
        public String ConfigKey { get; set; }
        public String ConfigValue { get; set; }
        public String ConfigType { get; set; }
        public String Remark { get; set; }
    }
}
