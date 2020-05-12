using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public class PageList<T>
    {
        public List<T> rows { get; set; }
        public int totalCount { get; set; }
        public PageList(List<T> rows, int totalCount)
        {
            this.rows = rows;
            this.totalCount = totalCount;
        }
    }
}
