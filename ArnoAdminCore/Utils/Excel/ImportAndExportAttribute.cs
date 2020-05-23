using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Utils.Excel
{
    public class ImportAndExportAttribute : ExcelAttribute
    {
        public ImportAndExportAttribute() { }
        public ImportAndExportAttribute(string description) : base(description) { }
    }
}
