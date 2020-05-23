using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Utils.Extension
{
    public static partial class Extensions
    {
        #region 转换为long
        public static long ParseToLong(this object obj, long defaultValue)
        {
            if (obj == null) return defaultValue;
            return ParseToLong(obj.ToString(), defaultValue);
        }
        public static long ParseToLong(this object obj)
        {
            return ParseToLong(obj, default);
        }
        public static long ParseToLong(this String str, long defaultValue)
        {
            if (String.IsNullOrWhiteSpace(str)) return defaultValue;
            try
            {
                long.TryParse(str, out defaultValue);
            }
            catch { }
            return defaultValue;
        }
        public static long ParseToLong(this String str)
        {
            return ParseToLong(str, default);
        }
        #endregion

        #region 转换为Int
        public static int ParseToInt(this object obj, int defaultValue)
        {
            if (obj == null) return defaultValue;
            return ParseToInt(obj.ToString(), defaultValue);
        }
        public static int ParseToInt(this object obj)
        {
            return ParseToInt(obj, default);
        }
        public static int ParseToInt(this String str, int defaultValue)
        {
            if (String.IsNullOrWhiteSpace(str)) return defaultValue;
            try
            {
                int.TryParse(str, out defaultValue);
            }
            catch { }
            return defaultValue;
        }
        public static int ParseToInt(this String str)
        {
            return ParseToInt(str, default);
        }
        #endregion

        #region 转换为Short
        public static short ParseToShort(this object obj, short defaultValue)
        {
            if (obj == null) return defaultValue;
            return ParseToShort(obj.ToString(), defaultValue);
        }
        public static short ParseToShort(this object obj)
        {
            return ParseToShort(obj, default);
        }
        public static short ParseToShort(this String str, short defaultValue)
        {
            if (String.IsNullOrWhiteSpace(str)) return defaultValue;
            try
            {
                short.TryParse(str, out defaultValue);
            }
            catch { }
            return defaultValue;
        }
        public static short ParseToShort(this String str)
        {
            return ParseToShort(str, default);
        }
        #endregion

        #region 转换为demical
        public static Decimal ParseToDecimal(this object obj, Decimal defaultValue)
        {
            if (obj == null) return defaultValue;
            return ParseToDecimal(obj.ToString(), defaultValue);
        }
        public static Decimal ParseToDecimal(this object obj)
        {
            return ParseToDecimal(obj, default);
        }
        public static Decimal ParseToDecimal(this String str, Decimal defaultValue)
        {
            if (String.IsNullOrWhiteSpace(str)) return defaultValue;
            try
            {
                Decimal.TryParse(str, out defaultValue);
            }
            catch { }
            return defaultValue;
        }
        public static Decimal ParseToDecimal(this String str)
        {
            return ParseToDecimal(str, default);
        }
        #endregion

        #region 转化为bool
       public static bool ParseToBool(this object obj, bool defaultValue)
        {
            if (obj == null) return defaultValue;
            return ParseToBool(obj.ToString(), defaultValue);
        }
        public static bool ParseToBool(this object obj)
        {
            return ParseToBool(obj, default);
        }
        public static bool ParseToBool(this String str, bool defaultValue)
        {
            if (String.IsNullOrWhiteSpace(str)) return defaultValue;
            try
            {
                bool.TryParse(str, out defaultValue);
            }
            catch { }
            return defaultValue;
        }
        public static bool ParseToBool(this String str)
        {
            return ParseToBool(str, default);
        }
        #endregion

        #region 转换为float
        public static float ParseToFloat(this object obj, float defaultValue)
        {
            if (obj == null) return defaultValue;
            return ParseToFloat(obj.ToString(), defaultValue);
        }
        public static float ParseToFloat(this object obj)
        {
            return ParseToFloat(obj, default);
        }
        public static float ParseToFloat(this String str, float defaultValue)
        {
            if (String.IsNullOrWhiteSpace(str)) return defaultValue;
            try
            {
                float.TryParse(str, out defaultValue);
            }
            catch { }
            return defaultValue;
        }
        public static float ParseToFloat(this String str)
        {
            return ParseToFloat(str, default);
        }
        #endregion

        #region 转换为double
        public static double ParseToDouble(this object obj, double defaultValue)
        {
            if (obj == null) return defaultValue;
            return ParseToDouble(obj.ToString(), defaultValue);
        }
        public static double ParseToDouble(this object obj)
        {
            return ParseToDouble(obj, default);
        }
        public static double ParseToDouble(this String str, double defaultValue)
        {
            if (String.IsNullOrWhiteSpace(str)) return defaultValue;
            try
            {
                double.TryParse(str, out defaultValue);
            }
            catch { }
            return defaultValue;
        }
        public static double ParseToDouble(this String str)
        {
            return ParseToDouble(str, default);
        }
        #endregion

        #region 转换为DateTime
        public static DateTime? ParseToDateTime(this object obj, DateTime? defaultValue)
        {
            if (obj == null) return defaultValue;
            return ParseToDateTime(obj.ToString(), defaultValue);
        }
        public static DateTime? ParseToDateTime(this object obj)
        {
            return ParseToDateTime(obj, default);
        }
        public static DateTime? ParseToDateTime(this string str, DateTime? defaultValue)
        {
            if (String.IsNullOrWhiteSpace(str)) return defaultValue;
            try
            {
                if (str.Contains("-") || str.Contains("/"))
                {
                    return DateTime.Parse(str);
                }
                else
                {
                    int length = str.Length;
                    switch (length)
                    {
                        case 4:
                            return DateTime.ParseExact(str, "yyyy", System.Globalization.CultureInfo.CurrentCulture);
                        case 6:
                            return DateTime.ParseExact(str, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);
                        case 8:
                            return DateTime.ParseExact(str, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        case 10:
                            return DateTime.ParseExact(str, "yyyyMMddHH", System.Globalization.CultureInfo.CurrentCulture);
                        case 12:
                            return DateTime.ParseExact(str, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
                        case 14:
                            return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                        default:
                            return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    }
                }
            }
            catch
            {
                return defaultValue;
            }
        }
        public static DateTime? ParseToDateTime(this string str)
        {
            return ParseToDateTime(str, default);
        }

        #endregion

        #region 转换为string
        /// <summary>
        /// 将object转换为string，若转换失败，则返回""。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ParseToString(this object obj)
        {
            try
            {
                if (obj == null || obj == DBNull.Value)
                {
                    return string.Empty;
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string ParseToStrings<T>(this object obj)
        {
            try
            {
                var list = obj as IEnumerable<T>;
                if (list != null)
                {
                    return string.Join(",", list);
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }

        }
        #endregion

        #region 强制转换类型
        /// <summary>
        /// 强制转换类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> CastSuper<TResult>(this IEnumerable source)
        {
            foreach (object item in source)
            {
                yield return (TResult)Convert.ChangeType(item, typeof(TResult));
            }
        }
        #endregion
    }
}
