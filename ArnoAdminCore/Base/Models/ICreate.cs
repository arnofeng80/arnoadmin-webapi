using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public interface ICreate
    {
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public void Create();
    }
}
