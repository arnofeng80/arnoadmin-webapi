using ArnoAdminCore.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public class DateRange
    {
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
