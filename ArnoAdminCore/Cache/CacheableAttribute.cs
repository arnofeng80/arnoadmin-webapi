using AspectCore.DynamicProxy;
using Newtonsoft.Json;
using NPOI.SS.Formula;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Cache
{
    public class CacheableAttribute : AbstractInterceptorAttribute
    {
        private static object lockObject = new object();
        public String Value { get; set; }
        public CacheableAttribute(String value) {
            this.Value = value;
        }

        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            lock(lockObject)
            {
                var cacheKey = GenerateCacheKey(context);
                var cacheValue = CacheFactory.Cache.GetHashFieldCache<object>(this.Value, cacheKey);
                if (cacheValue != null)
                {
                    context.ReturnValue = cacheValue;
                    return Task.CompletedTask;
                }
                var task = next(context);
                cacheValue = context.ReturnValue;
                CacheFactory.Cache.SetHashFieldCache<object>(this.Value, cacheKey, cacheValue);
                return task;
            }
        }

        private string GenerateCacheKey(AspectContext context)
        {
            int hashCode = 0;
            foreach (var param in context.Parameters)
            {
                hashCode = hashCode ^ param.GetHashCode();
            }

            return $"{context.ServiceMethod.DeclaringType.Name}:{context.ServiceMethod.Name}:{hashCode}";
        }
    }
}
