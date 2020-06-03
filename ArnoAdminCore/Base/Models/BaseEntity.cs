using ArnoAdminCore.Utils;
using ArnoAdminCore.Utils.Excel;
using ArnoAdminCore.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public class BaseEntity
    {
        [Description("創建人")]
        [Export]
        public long Id { get; set; }
        public string CreateBy { get; set; }
        [Description("創建時間")]
        [Export]
        public DateTime CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Deleted { get; set; }

        public void Create()
        {
            this.Id = IdGenerator.GetId();
            this.CreateTime = this.UpdateTime = DateTime.Now;
            this.CreateBy = this.UpdateBy = Current.Operator.LoginName;
            this.Deleted = 0;
        }

        public void Update()
        {
            this.UpdateTime = DateTime.Now;
            this.UpdateBy = Current.Operator.LoginName;
        }
    }
}
