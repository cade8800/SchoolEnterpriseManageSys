
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class RequestHelper
    {
        public static string GetIP()
        {
            string userHostAddress = "";
            var request = HttpContext.Current != null ? HttpContext.Current.Request : null;
            if (request != null)
            {
                //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
                userHostAddress = string.IsNullOrWhiteSpace(request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? "" : request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                //否则直接读取REMOTE_ADDR获取客户端IP地址 
                userHostAddress = string.IsNullOrWhiteSpace(userHostAddress) ? request.ServerVariables["REMOTE_ADDR"] : userHostAddress;
                //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP 
                userHostAddress = string.IsNullOrWhiteSpace(userHostAddress) ? request.UserHostAddress : userHostAddress;
                //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要） 
                //假如格式不正确，那么就返回127.0.0.1 
                userHostAddress = string.IsNullOrWhiteSpace(userHostAddress) || !IsIP(userHostAddress) ? "127.0.0.1" : userHostAddress;
            }
            return userHostAddress;
        } 

        public static string GetAllIps()
        {
            var request = HttpContext.Current != null ? HttpContext.Current.Request : null;
            string remote_addr = string.IsNullOrWhiteSpace(request.ServerVariables["REMOTE_ADDR"]) ? "" : request.ServerVariables["REMOTE_ADDR"];
            string xff = string.IsNullOrWhiteSpace(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? "" : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            return "remote_addr=" + remote_addr + "  xff=" + xff + "    UserHostAddress=" + request.UserHostAddress;
        }

        public static string GetRealRequestIP()
        {
            string userHostAddress = "";
            var request = HttpContext.Current != null ? HttpContext.Current.Request : null;
            if (request != null)
            {
                //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
                userHostAddress = string.IsNullOrWhiteSpace(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"])?"": HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                //否则直接读取REMOTE_ADDR获取客户端IP地址
                if (string.IsNullOrWhiteSpace(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
                if (string.IsNullOrWhiteSpace(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.UserHostAddress;
                }
                //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要） 
                if (string.IsNullOrWhiteSpace(userHostAddress) || !IsIP(userHostAddress)) //假如格式不正确，那么就返回127.0.0.1
                {
                    userHostAddress = "127.0.0.1";
                }
            }
            return userHostAddress;
        }

        private static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获取地址信息
        /// </summary>
        /// <returns></returns>
        public static string GetUrlPath()
        {
            string result = "";
            var request = HttpContext.Current != null ? HttpContext.Current.Request : null;
            if (request != null)
            {
                result = request.Url != null ? request.Url.ToString() : "";
            }
            return result;
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        public static string GetParamers()
        {
            string result = "";
            var request = HttpContext.Current != null ? HttpContext.Current.Request : null;
            try
            {
                if (request != null)
                {
                    var isPost = string.Compare(request.HttpMethod, "post", true) == 0;
                    if (isPost)
                    {
                        if (request.Form != null && request.Form.Count > 0)
                        {
                            for (int i = 0; i < request.Form.Count; i++)
                            {
                                result += string.Format("{0}:{1},", request.Form.GetKey(i), request.Form[i]);
                            }
                        }

                        if (request.InputStream != null && string.IsNullOrWhiteSpace(result))
                        {
                            // 不处理非 GET/FORM 请求
                            // result = ConverInputSteamToString(request.InputStream);
                            return result;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(result) && request.QueryString != null && request.QueryString.Count > 0)
                    {
                        result = JsonUtility.ToJson(request.QueryString, false);
                    }
                }
            }
            catch// (Exception ex)
            {

            }
            return result;
        }
        /// <summary>
        /// 转换输入流为字符串
        /// </summary>
        /// <param name="inputSteam"></param>
        /// <returns></returns>
        public static string ConverInputSteamToString(Stream inputSteam)
        {
            using (StreamReader sr = new StreamReader(inputSteam))
            {
                inputSteam.Position = 0;
                return sr != null ? sr.ReadToEnd() : "";
            }
        }

        /// <summary>
        /// 转换头部为字典类型返回
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetHeaders(List<string> keys = null)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var request = HttpContext.Current != null ? HttpContext.Current.Request : null;
            if (request != null && request.Headers != null)
            {
                if (keys != null && keys.Count > 0)
                {
                    foreach (var item in keys)
                    {
                        var vals = request.Headers.GetValues(item);
                        if (vals != null && vals.Length > 0)
                        {
                            result.Add(item, vals[0]);
                        }
                    }
                }
                else
                {
                    string key = string.Empty;
                    foreach (var item2 in request.Headers.Keys)
                    {
                        key = item2.ToString();
                        var vals2 = request.Headers.GetValues(key);
                        if (vals2 != null && vals2.Length > 0)
                        {
                            result.Add(key, vals2[0]);
                        }
                    }
                }
            }
            return result;
        }
    }
}
