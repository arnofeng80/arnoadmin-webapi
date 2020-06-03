using ArnoAdminCore.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Cache
{
    public class CacheFactory
    {
        private static ICache cache = null;
        private static readonly object lockHelper = new object();
        public static ICache Cache
        {
            get
            {
                if (cache == null)
                {
                    lock (lockHelper)
                    {
                        if (cache == null)
                        {
                            switch (GlobalContext.SystemConfig.CacheProvider)
                            {
                                //case "Redis": cache = new RedisCacheImp(); break;
                                default:
                                    cache = new MemoryCache();
                                    break;
                            }
                        }
                    }
                }
                return cache;
            }
        }
    }
}
