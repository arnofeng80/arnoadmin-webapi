using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public class BasePageSearch : BaseSearch
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageNum { get; set; } = 1;
    }
}
