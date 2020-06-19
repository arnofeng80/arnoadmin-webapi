using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public interface IUpdate
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public void Update();
    }
}
