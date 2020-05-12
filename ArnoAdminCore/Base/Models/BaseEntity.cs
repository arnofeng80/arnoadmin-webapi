using ArnoAdminCore.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Deleted { get; set; }

        public void Create()
        {
            this.Id = IdGenerator.GetId();
            this.CreateTime = this.UpdateTime = DateTime.Now;
            this.CreateBy = this.UpdateBy = 0;
            this.Deleted = 0;
        }
    }
}
