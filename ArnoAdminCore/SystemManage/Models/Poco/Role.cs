using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class Role : BaseEntity
    {
        public String RoleName { get; set; }
        public String RoleCode { get; set; }
        public int OrderNum { get; set; }
        public String DataScope { get; set; }
        public String Status { get; set; }
        public String Remark { get; set; }
        public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
        public virtual ICollection<RoleDept> RoleDepts { get; set; } = new List<RoleDept>();
    }
}
