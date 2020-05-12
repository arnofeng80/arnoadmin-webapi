using ArnoAdminCore.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Utils
{
    public class IdGenerator
    {
        private static Snowflake _snowflake;
        private static Snowflake GetSnowflake()
        {
            if (_snowflake == null)
            {
                _snowflake = new Snowflake(GlobalContext.SystemConfig.SnowFlakeWorkerId, 0, 0);
            }
            return _snowflake;
        }
        public static long GetId()
        {
            return GetSnowflake().NextId();
        }
        private int SnowFlakeWorkerId = GlobalContext.SystemConfig.SnowFlakeWorkerId;

        public static long GetUnixTimeStamp(DateTime dt)
        {
            long unixTime = ((DateTimeOffset)dt).ToUnixTimeMilliseconds();
            return unixTime;
        }
    }
}
