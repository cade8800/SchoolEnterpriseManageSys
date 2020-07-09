using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Kulv.Sems.API.MessageHandler;
using System.Net;
using System.IO;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class HttpClientHelper
    {
        /// <summary>
        /// 同步模拟Get请求  by:Eedc
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string HttpGet(string Url, string postDataStr = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// 获取get内容
        /// </summary> 
        /// <typeparam name="T">返回泛型类</typeparam>
        /// <param name="url">地址</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static T Get<T>(string url, object parameter = null, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (parameter != null)
                {
                    url = string.Format("{0}?{1}", url, parameter.ObjectToUrlParameter());
                }
                if (headers != null && headers.Count() > 0)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                var result = client.GetStringAsync(url).Result;
                if (result != null)
                {
                    return JsonUtility.FromJsonTo<T>(result);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 异步获取get内容
        /// </summary>
        /// <typeparam name="T">返回泛型类</typeparam>
        /// <param name="url">地址</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string url, object parameter = null)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                if (parameter != null)
                {
                    url = string.Format("{0}?{1}", url, parameter.ObjectToUrlParameter());
                }
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
            }
            return default(T);
        }

        /// <summary>
        /// 获取post内容
        /// </summary> 
        /// <typeparam name="T">返回泛型类</typeparam>
        /// <param name="url">地址</param>
        /// <param name="parameter">参数</param>
        /// <param name="headers">请求头集合</param>
        /// <returns></returns>
        public static T Post<T>(string url, object parameter, Dictionary<string, string> headers = null)
        {
            return PostAsync<T>(url, parameter, headers).Result;
        }

        /// <summary>
        /// 异步获取post内容
        /// </summary>
        /// <typeparam name="T">返回泛型类</typeparam>
        /// <param name="url">地址</param>
        /// <param name="parameter">参数</param>
        /// <param name="headers">请求头集合</param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(string url, object parameter, Dictionary<string, string> headers = null)
        {
            return await PostAsync<T>(url, parameter, false, headers);
        }

        /// <summary>
        /// 异步获取post内容
        /// </summary>
        /// <typeparam name="T">返回泛型类</typeparam>
        /// <param name="url">地址</param>
        /// <param name="parameter">参数</param>
        /// <param name="parameterInUrl">参数是否放在URL地址上</param>
        /// <param name="headers">请求头集合</param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(string url, object parameter, bool parameterInUrl = false, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != null && headers.Count() > 0)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                url = parameterInUrl ? string.Format("{0}?{1}", url, parameter.ObjectToUrlParameter()) : url;
                parameter = parameterInUrl ? null : parameter;
                var response = await client.PostAsJsonAsync<object>(url, parameter);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
            }
            return default(T);
        }

        public static T Post<T>(HttpClient client, string url, object parameter, Dictionary<string, string> headers = null)
        {
            client = client == null ? new HttpClient() : client;
            StringContent stringContent = new StringContent(parameter.ObjectToJson());
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (headers != null && headers.Count() > 0)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            var response = client.PostAsync(url, stringContent).Result.Content.ReadAsStringAsync().Result;
            client.Dispose();
            if (!string.IsNullOrWhiteSpace(response))
            {
                return JsonUtility.FromJsonTo<T>(response);
            }
            return default(T);
        }

        /// <summary>
        /// POST方式调用接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client">调用客户端</param>
        /// <param name="url">请求URL地址</param>
        /// <param name="parameter">请求URL参数</param>
        /// <returns></returns>
        public static T HttpPostMagApi<T>(string url, object parameter = null, bool needSecurity = false)
        {
            var api = new UriBuilder(url).Path;

            using (var httpClient = CreateHttpClient(client, api))
            {
                httpClient.Timeout = new TimeSpan(0, 5, 0);
                var json = parameter.ObjectToJson();
                if (needSecurity)
                {
                    json = EncryptionUtils.EncryptFormatStr(json);
                    //var apigeeHeaderKey = ConfigurationUtility.AppSetting("ApigeeHeaderKey");
                    //var apigeeHeaderValue = SecurityUtility.GetApigeeHearderValue();
                    //httpClient.DefaultRequestHeaders.Add(apigeeHeaderKey, apigeeHeaderValue);
                }
                StringContent stringContent = new StringContent(json);
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync(url, stringContent).Result.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(response))
                {
                    if (needSecurity)
                    {
                        response = EncryptionUtils.DecryptFormatStr(response);
                    }
                    return JsonUtility.FromJsonTo<T>(response);
                }
            }

            return default(T);
        }
        private static HttpClient CreateHttpClient(string client, string api)
        {
            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("SecurityKey", securityKey);
            return httpClient;
        }
        static string client = "kulvmarketingservice.yaochufa.com";
    }
}
