using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    /// <summary>
    /// 配置工具类
    /// </summary>
    public class ConfigurationUtility
    {
        //public bool ConfigUtilityUseSql {
        //    get {
        //        return AppSettingService.GetAppValue<bool>("ConfigUtilityUseSql", true);
        //    }
        //}

        #region 读取数据库配置连接字符串
        /// <summary>
        /// 读取数据库配置连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ConnectionString(string key)
        {
            var setting = ConfigurationManager.ConnectionStrings[key];
            return setting != null ? setting.ConnectionString : string.Empty;
        }

        /// <summary>
        /// 读取数据库配置连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ConnectionString(string key, string defaultValue)
        {
            var connectionString = ConnectionString(key);
            return !string.IsNullOrWhiteSpace(connectionString) ? connectionString : defaultValue;
        }
        #endregion

        #region 【非泛型版本】获取配置value

        /// <summary>
        /// 获取配置value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="configurPriority">优先级，不传取默认</param>
        /// <returns></returns>
        public static string AppSetting(string key, ConfigurPriority configurPriority = ConfigurPriority.Default)
        {
            
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取配置value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defauleValue">不配置时的默认value</param>
        /// <param name="configurPriority">优先级，不传取默认</param>
        /// <returns></returns>
        public static string AppSetting(string key, string defauleValue, ConfigurPriority configurPriority = ConfigurPriority.Default)
        {
            var value = AppSetting(key);
            return !string.IsNullOrWhiteSpace(value) ? value : defauleValue;
        }

        #endregion

        #region 【泛型版本】获取配置value
        /// <summary>
        /// 获取配置value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="configurPriority">优先级，不传取默认</param>
        /// <returns></returns>
        public static T AppSetting<T>(string key, ConfigurPriority configurPriority = ConfigurPriority.Default)
        {
            return AppSetting<T>(key, default(T));
        }

        /// <summary>
        /// 获取配置value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="defaultValue">不配置时的默认value</param>
        /// <param name="configurPriority">优先级，不传取默认</param>
        /// <returns></returns>
        public static T AppSetting<T>(string key, T defaultValue, ConfigurPriority configurPriority = ConfigurPriority.Default)
        {
            var value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : (T)Convert.ChangeType(value, typeof(T));
        }
        #endregion

    }

    /// <summary>
    /// 读配置的优先级
    /// </summary>
    public enum ConfigurPriority
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 配置文件
        /// </summary>
        WebConfig = 1,
        /// <summary>
        /// 数据库配置
        /// </summary>
        SqlSetting = 2
    }
}
