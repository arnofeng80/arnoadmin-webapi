using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArnoAdminCore.Utils
{
    public class ReflectionHelper
    {
        private static ConcurrentDictionary<string, object> dictCache = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 得到類裡面的屬性集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(Type type, string[] columns = null)
        {
            PropertyInfo[] properties = null;
            if (dictCache.ContainsKey(type.FullName))
            {
                properties = dictCache[type.FullName] as PropertyInfo[];
            }
            else
            {
                properties = type.GetProperties();
                dictCache.TryAdd(type.FullName, properties);
            }

            if (columns != null && columns.Length > 0)
            {
                //  按columns順序返回屬性
                var columnPropertyList = new List<PropertyInfo>();
                foreach (var column in columns)
                {
                    var columnProperty = properties.FirstOrDefault(p => p.Name == column);
                    if (columnProperty != null)
                    {
                        columnPropertyList.Add(columnProperty);
                    }
                }
                return columnPropertyList.ToArray();
            }
            else
            {
                return properties;
            }
        }
    }
}
