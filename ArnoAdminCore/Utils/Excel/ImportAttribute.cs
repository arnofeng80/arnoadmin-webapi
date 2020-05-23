using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Utils.Excel
{
    public class ImportAttribute : ExcelAttribute
    {
        public ImportAttribute() { }
        public ImportAttribute(string description) : base(description) { }
    }
}
