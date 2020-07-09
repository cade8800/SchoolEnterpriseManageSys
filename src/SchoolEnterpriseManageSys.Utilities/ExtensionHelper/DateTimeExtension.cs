using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    public static class DateTimeExtension
    {
        public static string TimeFromThen(this DateTime thenTime)
        {
            var timeNow = DateTime.Now;
            var totalSeconds = (timeNow - thenTime).TotalSeconds;
            return totalSeconds.ToString();
        }
        /// <summary>
        /// 把时间格式化成 yyyy-MM-dd ,为null时返回null
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string DateTimeFormatDay(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeFormatDay(dateTime.Value);
            }
            return null;
        }
        /// <summary>
        /// 把时间格式化成 yyyy-MM-dd ,为null时返回null
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string DateTimeFormatDay(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 把时间格式化成 yyyy-MM-dd hh:mm:ss,为null时返回null
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string DateTimeFormatSencond(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeFormatSencond(dateTime);
            }
            return null;           
        }
        /// <summary>
        /// 把时间格式化成 yyyy-MM-dd HH:mm:ss,为null时返回null
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string DateTimeFormatSencond(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
