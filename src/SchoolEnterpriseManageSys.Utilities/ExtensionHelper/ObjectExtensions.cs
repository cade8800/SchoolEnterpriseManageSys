


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    /// <summary>
    ///     通用类型扩展方法类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败返回类型的默认值 </returns>
        public static T CastTo<T>(this object value)
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = default(T);
            }

            return (T)result;
        }

        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            object result;
            Type type = typeof(T);
            try
            {
                result = type.IsEnum ? Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
            }
            catch
            {
                result = defaultValue;
            }
            return (T)result;
        }


        /// <summary>
        /// Distinct二次封装
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        public static T DeepCopy<T>(this T source) where T : class
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, source);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// convert object as target type. NOTE: Don't change the method name "As", it is used in other projects via reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T As<T>(this object value)
        {
            try
            {
                if (value is T) return (T)value;

                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter.CanConvertFrom(value.GetType()))
                {
                    return (T)converter.ConvertFrom(value);
                }

                converter = TypeDescriptor.GetConverter(value.GetType());
                if (converter.CanConvertTo(typeof(T)))
                {
                    return (T)converter.ConvertTo(value, typeof(T));
                }
            }
            catch
            {
            }

            return default(T);
        }

        public static object As(this object value, Type targetType, bool throwIfError = false)
        {
            Type underlyingType = null;

            try
            {
                if (value == null) return null;

                underlyingType = Nullable.GetUnderlyingType(targetType);
                Type usedTargetType = underlyingType ?? targetType;

                Type sourceType = value.GetType();
                if (sourceType == underlyingType) return value;

                TypeConverter converter = TypeDescriptor.GetConverter(usedTargetType);
                if (converter.CanConvertFrom(sourceType))
                {
                    return converter.ConvertFrom(value);
                }

                converter = TypeDescriptor.GetConverter(sourceType);
                if (converter.CanConvertTo(usedTargetType))
                {
                    return converter.ConvertTo(value, usedTargetType);
                }

                return value;
            }
            catch
            {
                if (throwIfError) throw;
                // We try best to resolve but it will hide the original error.
                if (underlyingType == null)
                {
                    return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
                }

                return null;
            }
        }


        #region HttpWebResponse类型扩展方法
        /// <summary>
        /// 直接取出Response的文本
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetResponseText(this HttpWebResponse response)
        {
            string result = string.Empty;
            if (response == null)
            {
                throw new NullReferenceException("response");
            }
            try
            {
                //获取网页类型，即ContentType
                string contentType = response.ContentType;
                if (!(contentType.ToUpper().StartsWith("TEXT") | contentType.ToUpper().Contains("JSON")))
                {
                    return string.Empty;
                }
                Stream desStream = null;
                // System.IO.Compression.GZipStream
                //判断网页是否经过压缩
                string contentEncoding = response.ContentEncoding;
                if (contentEncoding.ToUpper().Contains("GZIP"))
                {
                    desStream = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
                }
                else
                {
                    desStream = response.GetResponseStream();
                }

                using (desStream)
                {
                    //判断文字集，gb2312 UTF-8
                    //定义一个数据流，用于获取DesStream流数据
                    System.IO.StreamReader htmlStream = null;
                    string charSet = response.CharacterSet;
                    if (string.IsNullOrEmpty(charSet))
                    {
                        htmlStream = new System.IO.StreamReader(desStream, System.Text.Encoding.Default);
                    }
                    else
                    {
                        charSet = charSet.Trim().ToLower();
                        switch (charSet.ToLower())
                        {
                            case "gb2312":
                                htmlStream = new System.IO.StreamReader(desStream, System.Text.Encoding.GetEncoding("gb2312"));
                                break;
                            case "utf-8":
                            case "iso-8859-1":
                                htmlStream = new System.IO.StreamReader(desStream, System.Text.Encoding.UTF8);
                                break;
                            default:
                                htmlStream = new System.IO.StreamReader(desStream, System.Text.Encoding.Default);
                                break;
                        }
                    }
                    using (htmlStream)
                    {
                        string sourceCode = htmlStream.ReadToEnd();
                        return sourceCode;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("无法解析页面源码。", ex);
            }
        }
        #endregion

        #region HttpWebRequest类型扩展方法
        /// <summary>
        /// 可以通过字符串设置任何的RequestHeader
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetRequestHeader(this HttpWebRequest inputValue, string name, string value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                switch (name.ToUpper())
                {
                    case "ACCEPT":
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            inputValue.Accept = value;
                        }
                        break;
                    case "HOST":
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            inputValue.Host = value;
                        }
                        break;
                    case "REFERER":
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            inputValue.Referer = value;
                        }
                        break;
                    case "USER-AGENT":
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            inputValue.UserAgent = value;
                        }
                        break;
                    case "CONTENT-TYPE":
                        if (inputValue.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                inputValue.ContentType = value;
                            }
                        }
                        break;
                    case "CONTENT-LENGTH":
                        if (inputValue.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                long contentLength = 0;
                                long.TryParse(value, out contentLength);
                                inputValue.ContentLength = contentLength;
                            }
                        }
                        break;
                    case "IF-MODIFIED-SINCE":
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            DateTime tmpDate = new DateTime(1970, 1, 1);
                            DateTime.TryParse(value, out tmpDate);
                            inputValue.IfModifiedSince = Convert.ToDateTime(tmpDate);
                        }

                        break;
                    case "CONNECTION":
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            inputValue.KeepAlive = value.Equals("KEEP-ALIVE", StringComparison.OrdinalIgnoreCase);
                        }
                        break;
                    default:
                        if (value == null)
                        {
                            inputValue.Headers.Remove(name);
                        }
                        else
                        {
                            inputValue.Headers.Add(name, value);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 将headers中所有的键值对设置为RequestHeader
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="headers"></param>
        public static void SetRequestHeader(this HttpWebRequest inputValue, IDictionary<string, string> headers)
        {
            if (headers != null && headers.Count > 0)
            {
                foreach (string name in headers.Keys)
                {
                    inputValue.SetRequestHeader(name, headers[name]);
                }
            }
        }
        #endregion



        /// <summary>
        /// 显示手机号码前3位和后4位，把中间的部分用*号隐藏起来。
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string HidePhone(this string inputValue)
        {
            return inputValue.Substring(0, 3) + "****" + inputValue.Substring(7, 4); ;
        }

        /// <summary>
        /// 判断字符串是否ipv4
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static bool IsIpAdress(this string inputValue)
        {
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(
                    inputValue,
                    @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$",
                    RegexOptions.IgnoreCase
                );
            }

        }
        /// <summary>
        ///  获取字段的Description特性的描述文字
        /// </summary>
        /// <param name="enumeration"> </param>
        /// <returns> </returns>
        public static string ToDescription(this Type type, string propertyName)
        {
            PropertyInfo pinfo = type.GetProperty(propertyName);
            string des = "";
            //string name = pinfo.Name; //名称
            //object value = pinfo.GetValue(type, null);  //值
            if (pinfo != null)
            {
                Attribute attr = Attribute.GetCustomAttribute(pinfo, typeof(DescriptionAttribute));
                if (attr != null)
                {
                    des = ((DescriptionAttribute)attr).Description;// 属性值
                }
            }

            //if (pinfo.PropertyType.IsValueType || pinfo.PropertyType.Name.StartsWith("String"))
            //{

            //}
            return des;
        }

        #region 拓展方法：判断是否有值
        /// <summary>
        /// 拓展方法：判断是否有值且不是空字符
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this string obj)
        {
            if (obj == null || obj == "") return false;
            if (obj.Trim() == "") return false;
            else return true;
        }
        /// <summary>
        /// 拓展方法：判断是否为正数，一般用来判断某ID值是否有效
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsPlus(this int? obj)
        {
            if (!obj.HasValue || obj == 0) return false;
            else return true;
        }
        /// <summary>
        /// 拓展方法：判断是否为正数，一般用来判断某ID值是否有效
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsPlus(this long? obj)
        {
            if (!obj.HasValue || obj == 0) return false;
            else return true;
        }
        #endregion

    }
}