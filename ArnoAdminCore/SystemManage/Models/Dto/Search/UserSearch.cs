using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto.Search
{
    public class UserSearch : BasePageSearch
    {
        public String LoginName { get; set; }
        public String NameChn { get; set; }
        public String NameEng { get; set; }
        public String Status { get; set; }
    }
}
