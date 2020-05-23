using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Utils.Excel
{
    public class ExcelAttribute : Attribute
    {
        public string Description { get; set; }
        public ExcelAttribute() { }
        public ExcelAttribute(string description)
        {
            Description = description;
        }
    }
}
