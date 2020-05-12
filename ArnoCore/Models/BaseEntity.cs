using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoCore.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public int DelFlag { get; set; }
    }
}
