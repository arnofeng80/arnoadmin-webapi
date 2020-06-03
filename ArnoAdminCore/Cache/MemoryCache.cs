using ArnoAdminCore.Base;
using ArnoAdminCore.Utils;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArnoAdminCore.Cache
{
    public class MemoryCache : ICache
    {
        private static object lockObject = new object();
        private IMemoryCache cache = GlobalContext.ServiceProvider.GetService<IMemoryCache>();
        public bool SetCache<T>(string key, T value, DateTime? expireTime = null)
        {
            try
            {
                if (expireTime == null)
                {
                    return cache.Set<T>(key, value) != null;
                }
                else
                {
                    return cache.Set(key, value, (expireTime.Value - DateTime.Now)) != null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteWithTime(ex);
            }
            return false;
        }

        public void RemoveCache(string key)
        {
            cache.Remove(key);
        }

        public T GetCache<T>(string key)
        {
            return cache.Get<T>(key);
        }

        public object GetCache(string key)
        {
            return cache.Get(key);
        }

        #region Hash
        public void SetHashFieldCache(string key, string fieldKey, object fieldValue)
        {
            SetHashFieldCache<object>(key, fieldKey, fieldValue);
        }

        public void SetHashFieldCache<T>(string key, string fieldKey, T fieldValue)
        {
            lock(lockObject)
            {
                var dict = GetCache<Dictionary<string, T>>(key);
                if (dict == null)
                {
                    SetCache<Dictionary<string, T>>(key, new Dictionary<string, T> { { fieldKey, fieldValue } });
                }
                else
                {
                    dict.Add(fieldKey, fieldValue);
                }
            }
        }

        public object GetHashFieldCache(string key, string fieldKey)
        {
            return GetHashFieldCache<object>(key, fieldKey);
        }

        public T GetHashFieldCache<T>(string key, string fieldKey)
        {
            var dict = GetCache<Dictionary<string, T>>(key);
            if (dict != null && dict.ContainsKey(fieldKey))
            {
                return dict[fieldKey];
            }
            return default(T);
        }

        public void RemoveHashFieldCache(string key, string fieldKey)
        {
            RemoveHashFieldCache<object>(key, fieldKey);
        }

        public void RemoveHashFieldCache<T>(string key, string fieldKey)
        {
            var dict = GetCache<Dictionary<string, T>>(key);
            if(dict != null && dict.ContainsKey(fieldKey))
            {
                dict.Remove(fieldKey);
            }
        }

        public void RemoveHashFieldCache(string key)
        {
            cache.Remove(key);
        }
        #endregion
    }
}
