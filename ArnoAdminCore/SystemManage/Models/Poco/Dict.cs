using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class Dict : BaseEntity
    {
        public String DictCode { get; set; }
        public String DictLabel { get; set; }
        public String DictValue { get; set; }
        public int? DictSort { get; set; }
        public String Status { get; set; }
        public int IsDefault { get; set; }
        public String Remark { get; set; }
    }
}
