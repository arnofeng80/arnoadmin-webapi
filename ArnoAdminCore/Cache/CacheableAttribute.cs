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
        private Dictionary<string, object> cacheDict = new Dictionary<string, object>();
        private object lockObject = new object();
        public String Value { get; set; }
        public CacheableAttribute(String value) {
            this.Value = value;
        }

        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cacheKey = GenerateCacheKey(context);
            if (cacheDict.ContainsKey(cacheKey))
            {
                context.ReturnValue = cacheDict[cacheKey];
                return Task.CompletedTask;
            }
            var task = next(context);
            var cacheValue = context.ReturnValue;
            lock(lockObject)
            {
                if (!cacheDict.ContainsKey(cacheKey))
                {
                    cacheDict.Add(cacheKey, cacheValue);
                }
            }
            return task;
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
