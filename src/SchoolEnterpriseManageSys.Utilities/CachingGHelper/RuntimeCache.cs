using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolEnterpriseManageSys.Utilities.CachingGHelper
{
    public class RuntimeCache
    {
        readonly static System.Web.Caching.Cache Cache = HttpRuntime.Cache;

        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static object GetCache(string cacheKey)
        {
            return Cache[cacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string cacheKey, object objObject)
        {
            Cache.Insert(cacheKey, objObject);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static bool SetCache(string cacheKey, object objObject, int timeout)
        {
            Cache.Insert(cacheKey, objObject, null, DateTime.Now.AddMinutes(timeout), TimeSpan.Zero,
                System.Web.Caching.CacheItemPriority.Normal, null);
            return true;
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string cacheKey, object objObject, DateTime absoluteExpiration,
            TimeSpan slidingExpiration)
        {
            Cache.Insert(cacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static bool DelCache(string cacheKey)
        {
            Cache.Remove(cacheKey);
            return true;
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            var cacheEnum = Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                Cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }

}
