
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Xml;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace SchoolEnterpriseManageSys.Utilities.CachingGHelper
{
    internal  class Memcached
    {
        private static readonly bool IsUsedCache = ConfigurationManager.AppSettings["IsUseCached"] == "1";

        private static readonly MemcachedClient Mc = new MemcachedClient();



        /// <summary>
        /// 使用Add模式添加缓存，如果同名Key已存在返回false
        /// </summary>
        public static bool AddCacheExplicit(string key, object value, int minutes = 10)
        {
            if (!IsUsedCache)
                return false;
            if (minutes <= 0)
                return Mc.Store(StoreMode.Add, key, value);
            return Mc.Store(StoreMode.Add, key, value, TimeSpan.FromMinutes(minutes));
        }









        #region string类型缓存键的操作
        #region 添加缓存
        /// <summary>
        /// 添加缓存(键不存在则添加，存在则替换)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">缓存时间(分钟)</param>
        /// <returns></returns>
        public static bool AddCache(string key, object value, int minutes = 10)
        {
            if (!IsUsedCache)
                return false;

            //            if (minutes <= 0)
            //            {
            //#if DEBUG
            //                var result = Mc.ExecuteStore(StoreMode.Set, key, value);
            //                if (!result.Success)
            //                {
            //                    if (result.Exception != null)
            //                    {
            //                        throw new Exception(String.Format("Message:{0}, key:{1}",
            //                            result.Message,
            //                            key),
            //                            result.Exception);
            //                    }
            //                    else
            //                    {
            //                        throw new Exception(
            //                            String.Format("Message:{0}, Code:{1}, Key:{2}",
            //                            result.Message,
            //                            result.StatusCode,
            //                            key));
            //                    }
            //                }
            //#else
            //                return Mc.Store(StoreMode.Set, key, value);
            //#endif 
            //            }
            return Mc.Store(StoreMode.Set, key, value, DateTime.Now.AddMinutes(minutes));
        }
        #endregion

        #region 获取缓存
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回缓存，没有找到则返回null</returns>
        public static object GetCache(string key)
        {
            if (!IsUsedCache)
                return null;

            try
            {
                return Mc.Get(key);
            }
            catch (Exception)
            {
                return null;
            }


        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="keys">键</param>
        /// <returns>返回缓存，没有找到则返回null</returns>
        public static IDictionary<string, object> GetCache(IEnumerable<string> keys)
        {
            if (!IsUsedCache)
                return null;

            try
            {
                return Mc.Get(keys);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 是否存在该缓存
        /// <summary>
        /// 是否存在该缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool IsExists(string key)
        {
            if (!IsUsedCache)
                return false;

            return Mc.Get(key) != null;

        }
        #endregion

        #region 删除缓存(如果键不存在，则返回false)
        /// <summary>
        /// 删除缓存(如果键不存在，则返回false)
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>成功:true失败:false</returns>
        public static bool DelCache(string key)
        {
            if (!IsUsedCache)
                return false;

            return Mc.Remove(key);

        }
        #endregion
        #endregion

        #region 清空缓存
        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void FlushCache()
        {
            if (IsUsedCache)

                Mc.FlushAll();

        }
        #endregion
    }
}
