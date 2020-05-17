using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto.Router
{
    public class MenuRouter
    {
        public String Name { get; set; }
        public String Path { get; set; }
        public bool Hidden { get; set; }
        public String redirect { get; set; }
        public String Component { get; set; }
        public bool AlwaysShow { get; set; }
        public Meta Meta { get; set; }
    }
}
