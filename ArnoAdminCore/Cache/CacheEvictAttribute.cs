using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Cache
{
    public class CacheEvictAttribute : AbstractInterceptorAttribute
    {
        public String[] Value { get; set; }
        public CacheEvictAttribute(params String[] value)
        {
            this.Value = value;
        }

        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var task = next(context);
            foreach(string key in this.Value)
            {
                CacheFactory.Cache.RemoveHashFieldCache(key);
            }
            return task;
        }
    }
}
