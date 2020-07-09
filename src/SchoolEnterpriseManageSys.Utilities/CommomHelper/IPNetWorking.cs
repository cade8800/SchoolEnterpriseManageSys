using System;
using System.Net;
using System.Web;


namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class IPNetworking
    {
        /// <summary> 
        /// 取得客戶端主机地址 
        /// </summary> 
        public static string GetClientIP()
        {

            if (string.IsNullOrWhiteSpace(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                if (string.IsNullOrWhiteSpace(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]))
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            else
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

        }
    }
}