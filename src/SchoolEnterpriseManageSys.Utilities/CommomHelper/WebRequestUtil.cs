using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using SchoolEnterpriseManageSys.Utilities.ExtensionHelper;
using System.Web;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public static class WebRequestUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetAndResponse<T>(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "GET";
                request.Headers.Add("Accept-Encoding", "gzip");
                request.AllowAutoRedirect = true;
                request.Proxy = null;
                // 记录请求开始后到获得响应所耗时间(不包括数据传输)并重置计时器
                var counter = new Stopwatch();
                counter.Start();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    counter.Stop();
                    var requestCost = counter.ElapsedMilliseconds;
                    counter.Reset();
                    T result = default(T);
                    // 记录数据传输与本地处理数据所耗时间
                    counter.Start();
                    string json = response.GetResponseText();
                    counter.Stop();
                    var processCost = counter.ElapsedMilliseconds;
                    if(requestCost + processCost >= 1000)
                    {
                        LogRequestCostTime(url, requestCost, processCost);
                    }
                    result = JsonUtility.FromJsonTo<T>(json);
                    return result;
                }
            }
            finally
            {
                TryCatch(() =>
                {
                    request.Abort();
                });
            }
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="TP">自定义类型</typeparam>
        /// <param name="url">url</param>
        /// <param name="method">请求方式，暂时只支持Post。Get请调用其他</param>
        /// <param name="data">数据，类型为UserType，会自动序列化作为json将数据Post出去</param>
        /// <param name="contentType">RequestHeader:contentType</param>
        /// <returns>返回反序列化后的实例</returns>
        public static TR PostAndResponse<TR, TP>(string url, TP data, IDictionary<string, string> headers = null,
            string contentType = "application/json; charset=utf-8")
        {
            TR result = default(TR);
            string content = string.Empty;
            if (contentType.ToLower().Contains("json"))
            {
                content = JsonUtility.ToJson(data);
            }
            else
            {
                //未作处理，返回默认
                return default(TR);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.Headers.Add("Accept-Encoding", "gzip");
                request.ContentType = "application/json";
                request.AllowAutoRedirect = true;
                request.Proxy = null;
                if (headers != null)
                {
                    request.SetRequestHeader(headers);
                }
                byte[] byteData;
                if (contentType.ToLower().Contains("charset=utf-8"))
                {
                    byteData = Encoding.GetEncoding("UTF-8").GetBytes(content);
                    request.ContentType += "; charset=utf-8";
                }
                else
                {
                    //未作处理，返回默认
                    request = null;
                    return default(TR);
                }

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, (int)byteData.Length);
                    stream.Close();
                    // 记录请求开始后到获得响应所耗时间(不包括数据传输)并重置计时器
                    var counter = new Stopwatch();
                    counter.Start();
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        counter.Stop();
                        var requestCost = counter.ElapsedMilliseconds;
                        counter.Reset();
                        // 记录数据传输与本地处理数据所耗时间
                        counter.Start();
                        string json = response.GetResponseText();
                        counter.Stop();
                        var processCost = counter.ElapsedMilliseconds;
                        if(requestCost + processCost >= 1000)
                        {
                            LogRequestCostTime(url, requestCost, processCost);
                        }
                        result = JsonUtility.FromJsonTo<TR>(json);
                        return result;
                    }
                }
            }
            finally
            {
                TryCatch(() =>
                {
                    request.Abort();
                });
            }

        }

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="TP">自定义类型</typeparam>
        /// <param name="url">url</param>
        /// <param name="method">请求方式，暂时只支持Post。Get请调用其他</param>
        /// <param name="data">数据，类型为UserType，会自动序列化作为json将数据Post出去</param>
        /// <param name="contentType">RequestHeader:contentType</param>
        /// <returns>返回反序列化后的实例</returns>
        public static TR PostJsonAndResponse<TR, TP>(string url, TP data, out string jsonContent, out string painTextResult, IDictionary<string, string> headers = null)
        {
            TR result = default(TR);
            string content = string.Empty;

            content = JsonUtility.ToJson(data);
            jsonContent = content;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.Headers.Add("Accept-Encoding", "gzip");
                request.ContentType = "application/json";
                request.AllowAutoRedirect = true;
                request.Proxy = null;
                if (headers != null)
                {
                    request.SetRequestHeader(headers);
                }
                byte[] byteData;

                byteData = Encoding.GetEncoding("UTF-8").GetBytes(content);
                request.ContentType += "; charset=utf-8";


                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, (int)byteData.Length);
                    stream.Close();
                    // 记录请求开始后到获得响应所耗时间(不包括数据传输)并重置计时器
                    var counter = new Stopwatch();
                    counter.Start();
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        counter.Stop();
                        var requestCost = counter.ElapsedMilliseconds;
                        counter.Reset();
                        // 记录数据传输与本地处理数据所耗时间
                        counter.Start();
                        string json = response.GetResponseText();
                        counter.Stop();
                        var processCost = counter.ElapsedMilliseconds;
                        if (requestCost + processCost >= 1000)
                        {
                            LogRequestCostTime(url, requestCost, processCost);
                        }
                        painTextResult = json;
                        result = JsonUtility.FromJsonTo<TR>(json);
                        return result;
                    }
                }
            }
            finally
            {
                TryCatch(() =>
                {
                    request.Abort();
                });
            }
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="TP">自定义类型</typeparam>
        /// <param name="url">url</param>
        /// <param name="method">请求方式，暂时只支持Post。Get请调用其他</param>
        /// <param name="data">数据，类型为UserType，会自动序列化作为json将数据Post出去</param>
        /// <param name="contentType">RequestHeader:contentType</param>
        /// <returns>返回反序列化后的实例</returns>
        public static string PostJsonAndResponse<TP>(string url, TP data, out string jsonContent, IDictionary<string, string> headers = null)
        {
            string content = string.Empty;

            content = JsonUtility.ToJson(data);
            jsonContent = content;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.Headers.Add("Accept-Encoding", "gzip");
                request.ContentType = "application/json";
                request.AllowAutoRedirect = true;
                request.Proxy = null;
                if (headers != null)
                {
                    request.SetRequestHeader(headers);
                }
                byte[] byteData;

                byteData = Encoding.GetEncoding("UTF-8").GetBytes(content);
                request.ContentType += "; charset=utf-8";


                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, (int)byteData.Length);
                    stream.Close();
                    // 记录请求开始后到获得响应所耗时间(不包括数据传输)并重置计时器
                    var counter = new Stopwatch();
                    counter.Start();
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        counter.Stop();
                        var requestCost = counter.ElapsedMilliseconds;
                        counter.Reset();
                        // 记录数据传输与本地处理数据所耗时间
                        counter.Start();
                        string text = response.GetResponseText();
                        counter.Stop();
                        var processCost = counter.ElapsedMilliseconds;
                        if (requestCost + processCost >= 1000)
                        {
                            LogRequestCostTime(url, requestCost, processCost);
                        }
                        return text;
                    }
                }
            }
            finally
            {
                TryCatch(() =>
                {
                    request.Abort();
                });
            }
        }

        /// <summary>
        /// 带参数的请求
        /// </summary>
        /// <param name="rawUrl"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static T GetAndResponse<T>(string rawUrl, IDictionary<string, object> parms)
        {
            string append = string.Empty;
            if (parms != null && parms.Count > 0)
            {
                foreach (KeyValuePair<string, object> parm in parms)
                {
                    if (parm.Value != null)
                    {
                        append += parm.Key + "=" + parm.Value.ToString() + "&";
                    }
                }
            }
            append = append.TrimEnd('&');

            return WebRequestUtil.GetAndResponse<T>(rawUrl + "?" + append);
        }

        public static string GetAndResponseText(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Accept-Encoding", "gzip");
            request.AllowAutoRedirect = true;
            request.Proxy = null;
            HttpWebResponse response = null;
            string returnStr = string.Empty;
            try
            {
                // 记录请求开始后到获得响应所耗时间(不包括数据传输)并重置计时器
                var counter = new Stopwatch();
                counter.Start();
                response = (HttpWebResponse)request.GetResponse();
                counter.Stop();
                var requestCost = counter.ElapsedMilliseconds;
                counter.Reset();
                // 记录数据传输与本地处理数据所耗时间
                counter.Start();
                returnStr = response.GetResponseText();
                counter.Stop();
                var processCost = counter.ElapsedMilliseconds;
                if (requestCost + processCost >= 1000)
                {
                    LogRequestCostTime(url, requestCost, processCost);
                }
                response.Close();
            }
            catch// (Exception ex)
            {
                //获取失败
            }
            finally
            {
                response = null;
                TryCatch(() =>
                {
                    request.Abort();
                });
            }

            return returnStr;
        }

        public static string GetAndResponseText(string rawUrl, IDictionary<string, object> param)
        {
            string append = string.Empty;
            if (param != null && param.Count > 0)
            {
                foreach (KeyValuePair<string, object> parm in param)
                {
                    if (parm.Value != null)
                    {
                        append += parm.Key + "=" + parm.Value.ToString() + "&";
                    }
                }
            }
            append = append.TrimEnd('&');
            
            return WebRequestUtil.GetAndResponseText(rawUrl + "?" + append);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="TP">自定义类型</typeparam>
        /// <param name="url">url</param>
        /// <param name="method">请求方式，暂时只支持Post。Get请调用其他</param>
        /// <param name="data">数据</param>
        /// <returns>返回反序列化后的实例</returns>
        public static TR PostFormDataAndResponse<TR, TP>(string url, TP data, IDictionary<string, string> headers = null)
        {
            TR result = default(TR);
            string content = UrlHelper.ObjectToUrlParameter(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.Headers.Add("Accept-Encoding", "gzip");
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = true;
                request.Proxy = null;
                if (headers != null)
                {
                    request.SetRequestHeader(headers);
                }
                byte[] byteData;
                byteData = Encoding.GetEncoding("UTF-8").GetBytes(content);
                request.ContentType += "; charset=utf-8";

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, (int)byteData.Length);
                    stream.Close();
                    // 记录请求开始后到获得响应所耗时间(不包括数据传输)并重置计时器
                    var counter = new Stopwatch();
                    counter.Start();
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        counter.Stop();
                        var requestCost = counter.ElapsedMilliseconds;
                        counter.Reset();
                        // 记录数据传输与本地处理数据所耗时间
                        counter.Start();
                        string json = response.GetResponseText();
                        counter.Stop();
                        var processCost = counter.ElapsedMilliseconds;
                        if (requestCost + processCost >= 1000)
                        {
                            LogRequestCostTime(url, requestCost, processCost);
                        }
                        result = JsonUtility.FromJsonTo<TR>(json);
                        return result;
                    }
                }
            }
            finally
            {
                TryCatch(() =>
                {
                    request.Abort();
                });
            }
        }


        private static void TryCatch(Action action)
        {
            try
            {
                action();
            }
            catch// (Exception ex)
            { }
        }

        public static TR PostAndResponse<TR>(string url, string content, IDictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded; charset=utf-8")
        {
            TR result = default(TR);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.Headers.Add("Accept-Encoding", "gzip");
                request.ContentType = contentType;
                request.AllowAutoRedirect = true;
                request.Proxy = null;
                if (headers != null)
                {
                    request.SetRequestHeader(headers);
                }
                byte[] byteData;
                if (contentType.ToLower().Contains("charset=utf-8"))
                {
                    byteData = Encoding.GetEncoding("UTF-8").GetBytes(content);
                    request.ContentType += "; charset=utf-8";
                }
                else
                {
                    //未作处理，返回默认
                    request = null;
                    return default(TR);
                }

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, (int)byteData.Length);
                    stream.Close();
                    // 记录请求开始后到获得响应所耗时间(不包括数据传输)并重置计时器
                    var counter = new Stopwatch();
                    counter.Start();
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        counter.Stop();
                        var requestCost = counter.ElapsedMilliseconds;
                        counter.Reset();
                        // 记录数据传输与本地处理数据所耗时间
                        counter.Start();
                        string json = response.GetResponseText();
                        counter.Stop();
                        var processCost = counter.ElapsedMilliseconds;
                        if (requestCost + processCost >= 1000)
                        {
                            LogRequestCostTime(url, requestCost, processCost);
                        }
                        result = JsonUtility.FromJsonTo<TR>(json);
                        return result;
                    }
                }
            }
            finally
            {
                TryCatch(() =>
                {
                    request.Abort();
                });
            }

        }

        /// <summary>
        /// form post上传文件
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="file">文件流</param>
        /// <param name="fileName">文件名</param>
        /// <param name="simpleFormData">表单数据(非文件)</param>
        /// <param name="fileParamName">文件接收参数名</param>
        /// <returns></returns>
        public static string UploadFile(string url, Stream file, string fileName, Dictionary<string, object> simpleFormData = null, string fileParamName = "file")
        {
            var result = string.Empty;
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            var boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            var wr = (HttpWebRequest) WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = CredentialCache.DefaultCredentials;

            var rs = wr.GetRequestStream();

            var formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (simpleFormData.OpenSafe().Any())
            {
                foreach (var key in simpleFormData.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    var formitem = string.Format(formdataTemplate, key, simpleFormData[key]);
                    var formitembytes = Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }

            rs.Write(boundarybytes, 0, boundarybytes.Length);

            var headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            var header = string.Format(headerTemplate, fileParamName, fileName, MimeHelper.GetMimeType(fileName.Substring(fileName.LastIndexOf('.') + 1)));
            var headerbytes = Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);


            var buffer = new byte[4096];
            var bytesRead = 0;
            while ((bytesRead = file.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            file.Close();

            var trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                var stream2 = wresp.GetResponseStream();
                var reader2 = new StreamReader(stream2);
                
                result = reader2.ReadToEnd();
            }
            catch// (Exception ex)
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }

            return result;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlSegment"></param>
        /// <returns></returns>
        public static Stream DownloadFile(string url, Dictionary<string, object> urlSegment = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }
            if (urlSegment.OpenSafe().Any())
            {
                url += "&"+urlSegment.MapToUrlString();
            }

            var request = (HttpWebRequest)WebRequest.Create(url);
         // var reqStream = request.GetRequestStream();
           return request.GetResponse().GetResponseStream();
        }

        public static String MapToUrlString(this IDictionary<string, object> map)
        {
            if (map == null)
            {
                return string.Empty;
            }
            String result = String.Empty;
            foreach (var o in map)
            {
                result += o.Key + "=" + HttpUtility.UrlEncode((o.Value ?? "").ToString()) + "&";
            }
            return result.TrimEnd('&');
        }

        /// <summary>
        /// 记录访问远程地址的耗时时间到Info级别日志中
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="requestCost">请求开始到获得相应所花费时间</param>
        /// <param name="processCost">获取响应数据到处理响应数据所花费时间</param>
        private static void LogRequestCostTime(string url, long requestCost, long processCost)
        {
            //TODO LogHelper.Log4Logger.Info(String.Format("{0}访问耗时情况：总耗时{1}毫秒，其中请求开始到获得响应历时{2}毫秒，响应数据传输与本地处理历时{3}毫秒", url, requestCost + processCost, requestCost, processCost));
        }
    }
}
