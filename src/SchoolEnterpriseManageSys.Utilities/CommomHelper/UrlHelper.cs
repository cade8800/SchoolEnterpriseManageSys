using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public static class UrlHelper
    {
        public static string ObjectToUrlParameter(this object obj)
        {
            if (obj == null) return string.Empty;
            var properties = obj.GetType().GetProperties();
            List<object> list = new List<object>();
            StringBuilder psb = new StringBuilder();
            string v = string.Empty;
            foreach (var p in properties)
            {
                var value = p.GetValue(obj, null);
                v = value != null ? value.ToString() : string.Empty;
                var type = p.PropertyType.FullName;
                var index = type.IndexOf("[");
                // 当值为复杂类型时(判断依据：v返回的值跟type值类似，都是表示类路径的)
                if (value != null && index > 0 && v.EndsWith("]") && type.EndsWith("]") &&
                    v.Length > index && v.Substring(0, index).Equals(type.Substring(0, index)))
                {
                    v = JsonConvert.SerializeObject(value);
                    // 如果是数组，去掉数组的前后中括号
                    if (v.StartsWith("[") && v.EndsWith("]"))
                    {
                        v = v.Substring(1);
                        v = v.Substring(0, v.Length - 1);
                    }
                    // 如果里面包含的是字符串，去掉括号(字符串里面包含逗号时会引起歧义)
                    if (v.StartsWith("\"") && v.EndsWith("\""))
                    {
                        v = v.Trim('"').Replace("\",\"", ",");
                    }
                }

                // UrlEncode
                //foreach (Attribute attr in p.GetCustomAttributes(true))
                //{
                //    if (attr is Attributes.FieldEncodingAttribute)
                //    {
                //        Attributes.FieldEncodingAttribute a = attr as Attributes.FieldEncodingAttribute;
                //        if (a.Encod.ToLower() == "urlencode")
                //        {
                //            v = HttpUtility.UrlEncode(v);
                //        }
                //        break;
                //    }
                //}
                v = v.Replace("&", "%26");
                psb.AppendFormat("{0}={1}&", new object[] { p.Name, v });
            }
            return psb.ToString().TrimEnd(new char[] { '&' });
        }

        public static string ObjectToQuery<T>(this T obj, bool isAddEmptyValue = true) where T : class
        {
            if (obj == null)
            {
                return "";
            }
            var properties = obj.GetType().GetProperties();
            var list = new List<string>();
            foreach (var item in properties)
            {
                var proValue = item.GetValue(obj, null);
                if ((proValue != null && !string.IsNullOrEmpty(proValue.ToString())) || isAddEmptyValue)
                {
                    var value = proValue != null ? proValue.ToString() : "";
                    value = value.Replace("+", "%20");
                    list.Add(item.Name + "=" + value);
                }
            }

            return string.Join("&", list);
        }

        public static T QueryToObject<T>(string requestParam) where T : new()
        {
            T param = new T();
            var dicParam = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(requestParam))
            {
                var arry = requestParam.TrimStart('?').Split('&');
                foreach (var item in arry)
                {
                    var keyValus = item.Split('=');
                    if (!string.IsNullOrEmpty(keyValus[0]))
                    {
                        dicParam.Add(keyValus[0], keyValus[1]);
                    }
                }
                var props = typeof(T).GetProperties();
                foreach (var p in props)
                {
                    var name = p.Name;
                    if (!string.IsNullOrEmpty(name) && dicParam.ContainsKey(name))
                    {
                        p.SetValue(param, Convert.ChangeType(dicParam[name], p.PropertyType));
                    }
                }
            }
            return param;
        }

        public static string GetQueryFromStream()
        {
            using (Stream s = System.Web.HttpContext.Current.Request.InputStream)
            {
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string requestParam = Encoding.UTF8.GetString(b);

                return requestParam;
            }
        }

        /// <summary>
        /// 获取请求post参数字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetFormParams()
        {
            var param = System.Web.HttpContext.Current.Request.Form;
            var dicParam = new Dictionary<string, string>();
            foreach (var item in param.AllKeys)
            {
                if (!string.IsNullOrEmpty(item) && !dicParam.ContainsKey(item))
                {
                    dicParam.Add(item, param[item]);
                }
            }

            return dicParam;
        }

        public static string GetFormParamString()
        {
            var param = GetFormParams();
            var query = param.OrderBy(x => x.Key).ToDictionary(o => o.Key, p => p.Value);
            return DictionaryToQuery(query);
        }

        /// <summary>
        /// 获取请求get参数字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetQueryParams()
        {
            var param = System.Web.HttpContext.Current.Request.QueryString;
            var dicParam = new Dictionary<string, string>();
            foreach (var item in param.AllKeys)
            {
                if (!string.IsNullOrEmpty(item) && !dicParam.ContainsKey(item))
                {
                    dicParam.Add(item, param[item]);
                }
            }

            return dicParam;
        }

        public static string GetQueryParamString()
        {
            var param = GetQueryParams();
            var query = param.OrderBy(x => x.Key).ToDictionary(o => o.Key, p => p.Value);
            return DictionaryToQuery(query);
        }

        public static string DictionaryToQuery(Dictionary<string, string> query)
        {
            List<string> list = new List<string>();
            foreach (var item in query.Keys)
            {
                list.Add(item + "=" + query[item]);
            }

            return string.Join("&", list);
        }
    }
}
