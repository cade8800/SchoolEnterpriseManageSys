using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolEnterpriseManageSys.Utilities.CachingGHelper
{
    /// <summary>
    /// 缓存帮助类，可以增加不同缓存技术
    /// </summary>
    public class CacheHelper
    {

        #region 静态版本

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存内容</param>
        /// <param name="timeOut">缓存过期时间，单位分钟,默认十分钟</param>
        /// <param name="cacheType">缓存类型（默认是Memcached）</param>
        /// <returns></returns>
        public static bool Set(string key, object value, int timeOut = 10, CacheTypeEnum cacheType = CacheTypeEnum.Default)
        {
            if (IsNoCache()) return true;
            if (string.IsNullOrWhiteSpace(key)) return false;
            switch (cacheType)
            {
                case CacheTypeEnum.Memcached:
                case CacheTypeEnum.Default:
                default:
                    return Memcached.AddCache(key, value, timeOut);
                case CacheTypeEnum.HttpRuntime:
                    return RuntimeCache.SetCache(key, value, timeOut);
            }
        }

        /// <summary>
        /// 获取缓存泛型版本
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="cacheType">缓存类型（默认是Memcached）</param>
        /// <returns>缓存对象（实际类型）</returns>
        public static T Get<T>(string key, Func<T> func, int timeOut = 10, CacheTypeEnum cacheType = CacheTypeEnum.Default) where T : class
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            T result = null;
            if (!Refresh() && !IsNoCache())
            {
                switch (cacheType)
                {
                    case CacheTypeEnum.Memcached:
                    case CacheTypeEnum.Default:
                    default:
                        {
                            var data = Memcached.GetCache(key);
                            result = data != null ? data as T : null;
                        }
                        break;
                    case CacheTypeEnum.HttpRuntime:
                        {
                            var httpRuntimeData = RuntimeCache.GetCache(key);
                            result = httpRuntimeData != null ? httpRuntimeData as T : null;
                        } break;
                }
            }
            if (result == null)
            {
                result = func();
                if (result != null)
                {
                    CacheHelper.Set(key, result, timeOut, cacheType);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取缓存泛型版本
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="cacheType">缓存类型（默认是Memcached）</param>
        /// <returns>缓存对象（实际类型）</returns>
        public static T Get<T>(string key, CacheTypeEnum cacheType = CacheTypeEnum.Default) where T : class
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            if (Refresh() || IsNoCache())
            {
                return null;
            }
            switch (cacheType)
            {
                case CacheTypeEnum.Memcached:
                case CacheTypeEnum.Default:
                default:
                    var data = Memcached.GetCache(key);
                    return data != null ? data as T : null;
                case CacheTypeEnum.HttpRuntime:
                    var httpRuntimeData = RuntimeCache.GetCache(key);
                    return httpRuntimeData != null ? httpRuntimeData as T : null;
            }
        }

        /// <summary>
        /// 获取缓存泛型版本
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="cacheType">缓存类型（默认是Memcached）</param>
        /// <returns>缓存对象（实际类型）</returns>
        public static Dictionary<string, T> Get<T>(IEnumerable<string> keys, CacheTypeEnum cacheType = CacheTypeEnum.Default) where T : class
        {
            if (keys == null) return new Dictionary<string, T>(); ;
            if (Refresh() || IsNoCache())
            {
                return new Dictionary<string,T>();
            }
            switch (cacheType)
            {
                case CacheTypeEnum.Memcached:
                case CacheTypeEnum.Default:
                default:
                    {
                        var data = Memcached.GetCache(keys);
                        if (data != null)
                        {
                            Dictionary<string, T> dic = new Dictionary<string, T>();
                            foreach (var item in data)
                            {
                                T t = item.Value as T;
                                dic.Add(item.Key, t);
                            }
                            return dic;
                        }
                        else
                        {
                            return new Dictionary<string, T>();
                        }
                    }
                case CacheTypeEnum.HttpRuntime:
                    {
                        Dictionary<string, T> dic = new Dictionary<string, T>();
                        foreach (var key in keys)
                        {
                            var httpRuntimeData = RuntimeCache.GetCache(key);
                            var t= httpRuntimeData != null ? httpRuntimeData as T : null;
                            dic.Add(key, t);
                        }
                        return dic;
                    }
            }
        }


        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="cacheType">缓存类型（默认是Memcached）</param>
        /// <returns></returns>
        public static object Get(string key, CacheTypeEnum cacheType = CacheTypeEnum.Default)
        {
            if (string.IsNullOrWhiteSpace(key)) return null;
            if (Refresh() || IsNoCache())
            {
                return null;
            }
            switch (cacheType)
            {
                case CacheTypeEnum.Memcached:
                case CacheTypeEnum.Default:
                default:
                    return Memcached.GetCache(key);
                case CacheTypeEnum.HttpRuntime:
                    return RuntimeCache.GetCache(key);
            }
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="cacheType">缓存类型（默认是Memcached）</param>
        /// <returns></returns>
        public static bool Remove(string key, CacheTypeEnum cacheType = CacheTypeEnum.Default)
        {
            if (string.IsNullOrWhiteSpace(key)) return false;
            switch (cacheType)
            {
                case CacheTypeEnum.Memcached:
                case CacheTypeEnum.Default:
                default:
                    return Memcached.DelCache(key);
                case CacheTypeEnum.HttpRuntime:
                    return RuntimeCache.DelCache(key);
            }
        }

        #endregion

        public static bool Refresh()
        {
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null && (HttpContext.Current.Request["refreshCache"] == "true" || HttpContext.Current.Request["refreshCache"] == "1"))
                {
                    return true;
                }
            }
            catch { }

            return false;
        }

        public static bool IsNoCache()
        {
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null && (HttpContext.Current.Request["noCache"] == "true" || HttpContext.Current.Request["noCache"] == "1"))
                {
                    return true;
                }
            }
            catch { }

            return false;
        }

    }

    #region 缓存类型枚举
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheTypeEnum
    {
        [Description("默认")]
        Default,
        [Description("Memcached")]
        Memcached,
        [Description("redis")]
        Redis,
        [Description("HttpRuntime")]
        HttpRuntime,
    }

    #endregion

    #region 缓存类型枚举
    /// <summary>
    /// 缓存使用类型
    /// </summary>
    public enum CacheUsingTypeEnum
    {
        
    }

    #endregion
}
