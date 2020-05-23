using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Utils.Excel
{
    public class ExportAttribute : ExcelAttribute
    {
        public ExportAttribute() { }
        public ExportAttribute(string description) : base(description) { }
    }
}
