using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base
{
    public class GlobalContext
    {
        public static SystemConfig SystemConfig { get; set; }
        public static IWebHostEnvironment HostingEnvironment { get; set; }
    }
}
