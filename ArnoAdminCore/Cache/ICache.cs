using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Cache
{
    public interface ICache
    {
        bool SetCache<T>(string key, T value, DateTime? expireTime = null);
        T GetCache<T>(string key);
        object GetCache(string key);
        void RemoveCache(string key);
        #region Hash
        void SetHashFieldCache(string key, string fieldKey, object fieldValue);
        void SetHashFieldCache<T>(string key, string fieldKey, T fieldValue);
        object GetHashFieldCache(string key, string fieldKey);
        T GetHashFieldCache<T>(string key, string fieldKey);
        void RemoveHashFieldCache(string key, string fieldKey);
        void RemoveHashFieldCache<T>(string key, string fieldKey);
        void RemoveHashFieldCache(string key);
        #endregion
    }
}
