using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto.Search
{
    public class DictSearch : BasePageSearch
    {
        public String DictCode { get; set; }
        public String DictLabel { get; set; }
        public String Status { get; set; }
    }
}
