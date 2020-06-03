using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base
{
    public class GlobalContext
    {
        public static IServiceCollection Services { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }
        public static SystemConfig SystemConfig { get; set; }
        public static IWebHostEnvironment HostingEnvironment { get; set; }
    }
}
