using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.StringHelper
{
    public static class ValidationExtentions
    {
        public static bool CheckPhone(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;
            if (phone.Length > 11)
                return false;
            return CommonRegex.PhoneRegex.IsMatch(phone);
        }

        public static bool CheckPhone2(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;
            if (phone.Length > 11)
                return false;
            return CommonRegex.PhoneRegex2.IsMatch(phone);
        }

        public static bool CheckNum(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return CommonRegex.NumberRegex.IsMatch(str);
        }
        public static bool CheckInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            int _result;
            return int.TryParse(str, out _result);
        }
        public static bool CheckEmail(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return CommonRegex.EmailRegex.IsMatch(str);
        }

        public static string MatchChinese(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Match match = CommonRegex.ChineseRegex.Match(str);
                return match.Value;
            }
            return string.Empty;
        }

        public static string RemoveIllegalChars(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.ToLower().Replace("<", " ").Replace(">", " ").Replace("--", "").Replace("/*", "").Replace("*/", "").Replace("drop", "").Replace("insert", "").Replace("update", "").Replace("delete", "");
            }
            return str;
        }

        #region unicode码转换为汉字（国标）
        /// <summary>
        /// unicode码转换为汉字（国标）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnicodeToGB(this string str)
        {
            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        //将unicode字符转为10进制整数，然后转为char中文字符  
                        outStr += (char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber);
                    }
                }
                catch (FormatException)
                {
                    //outStr = ex.Message;
                }
            }
            return outStr;
        }
        #endregion

        #region 汉字（国标）转换为unicode码
        /// <summary>
        /// 汉字（国标）转换为unicode码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GBToUnicode(this string str)
        {
            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    //将中文字符转为10进制整数，然后转为16进制unicode字符  
                    outStr += "\\u" + ((int)str[i]).ToString("x");
                }
            }
            return outStr;
        }
        #endregion

    }
}
