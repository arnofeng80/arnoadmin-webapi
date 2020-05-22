using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public class BaseSearch
    {
        /// <summary>
        /// 排序列
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string SortType { get; set; }
    }
}
