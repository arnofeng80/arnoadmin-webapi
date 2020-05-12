using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base
{
    public class SystemConfig
    {
        public int SnowFlakeWorkerId { get; set; }
        public string DBProvider { get; set; }
        public string DBConnectionString { get; set; }
        public string DBCommandTimeout { get; set; }
        public string CacheProvider { get; set; }
        public string RedisConnectionString { get; set; }
    }
}
