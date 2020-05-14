using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class Menu : BaseEntity
    {
        public long ParentId { get; set; }
        public String MenuCode { get; set; }
        public String MenuName { get; set; }
        public int OrderNum { get; set; }
        public String Path { get; set; }
        public String Component { get; set; }
        public String IsFrame { get; set; }
        public String MenuType { get; set; }
        public String Visible { get; set; }
        public String Status { get; set; }
        public String Perms { get; set; }
        public String Icon { get; set; }
        public String Remark { get; set; }
        public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
    }
}
