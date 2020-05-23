using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Utils.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Poco
{
    public class User : BaseEntity
    {
        public long DeptId { get; set; }
        [Description("登入名")]
        [Export]
        public String LoginName { get; set; }
        [Description("中文名")]
        [Export]
        public String NameChn { get; set; }
        [Description("英文名")]
        [Export]
        public String NameEng { get; set; }
        public String UserType { get; set; }
        public String Email { get; set; }
        [Description("內線電話")]
        [Export]
        public String InternalPhone { get; set; }
        [Description("手機")]
        [Export]
        public String Mobile { get; set; }
        [Description("性別")]
        [Export]
        public String Gender { get; set; }
        public String Password { get; set; }
        [Description("狀態")]
        [Export]
        public String Status { get; set; }
        public String LoginIp { get; set; }
        public DateTime? LoginDate { get; set; }
        [Description("備注")]
        [Export]
        public String Remark { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}
