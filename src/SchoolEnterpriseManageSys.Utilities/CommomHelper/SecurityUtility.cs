using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class SecurityUtility
    {
        //SecurityKey 加密解密
        private static string DesKey = "f5qa9aEb";
        private static string DesIv = "HYHIM96C";
        //SecurityKey里面的userData加密解密
        private static string UserDataDesKey = "f5qHIMEb";
        private static string UserDataDesIv = "HY5qa96C";
        //Header 加密解密
        private static readonly string HeaderDesKey = "5qHIcryM";
        private static readonly string HeaderDesIv = "Ycry5qa9";
        public static readonly string ApigeeHeaderPrefix = "bGu29SFA6EdzLWIAS9evzjQg";

        static SecurityUtility()
        {
            if (!string.IsNullOrEmpty(ConfigurationUtility.AppSetting("SecurityKeyDesKey")))
            {
                DesKey = ConfigurationUtility.AppSetting("SecurityKeyDesKey");
            }
            if (!string.IsNullOrEmpty(ConfigurationUtility.AppSetting("SecurityKeyDesIv")))
            {
                DesIv = ConfigurationUtility.AppSetting("SecurityKeyDesIv");
            }

            if (!string.IsNullOrEmpty(ConfigurationUtility.AppSetting("UserDataDesKey")))
            {
                UserDataDesKey = ConfigurationUtility.AppSetting("UserDataDesKey");
            }
            if (!string.IsNullOrEmpty(ConfigurationUtility.AppSetting("UserDataDesIv")))
            {
                UserDataDesIv = ConfigurationUtility.AppSetting("UserDataDesIv");
            }
        }


        public static string EncryptSecurityKey(string input)
        {
            return SchoolEnterpriseManageSys.Utilities.DEncryptHelper.Encrypt.DESEncrypt(input, DesKey, DesIv);
        }

        public static string DecryptSecurityKey(string input)
        {
            return SchoolEnterpriseManageSys.Utilities.DEncryptHelper.Encrypt.DESDecrypt(input, DesKey, DesIv);
        }
        private static string EncryptUserData(string input)
        {
            return SchoolEnterpriseManageSys.Utilities.DEncryptHelper.Encrypt.DESEncrypt(input, UserDataDesKey, UserDataDesIv);
        }

        private static string DecryptUserData(string input)
        {
            return SchoolEnterpriseManageSys.Utilities.DEncryptHelper.Encrypt.DESDecrypt(input, UserDataDesKey, UserDataDesIv);
        }

        public static string EncryptHeader(string input)
        {
            return SchoolEnterpriseManageSys.Utilities.DEncryptHelper.Encrypt.DESEncrypt(input, HeaderDesKey, HeaderDesIv);
        }

        public static string DecryptHeader(string input)
        {
            return SchoolEnterpriseManageSys.Utilities.DEncryptHelper.Encrypt.DESDecrypt(input, HeaderDesKey, HeaderDesIv);
        }

        public static string GetApigeeHearderValue()
        {
            //明文串= bGu29SFA6EdzLWIAS9evzjQg#2015-01-12 12:59:01
            string source = string.Concat(ApigeeHeaderPrefix, "#", DateTime.UtcNow);
            return DecryptHeader(source);
        }

        public static string GetSecurityKey(SecurityData data)
        {
            string userdata = Uri.EscapeDataString(SecurityUtility.EncryptUserData(data.UserData));
            string input = string.Format("{0}#{1}#{2}#{3}#{4}#{5}", data.UserID,data.Expiration, userdata, data.Remember,data.UserPhone,data.UserName);
            string key = SecurityUtility.EncryptSecurityKey(input);
            //base on http://blogs.msdn.com/b/yangxind/archive/2006/11/09/don-t-use-net-system-uri-unescapedatastring-in-url-decoding.aspx
            return Uri.EscapeDataString(key);
        }
		/// <summary>
		/// BDapp-GetBdSecurityKey
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string GetBdSecurityKey(BdSecurityData data) {
			string userdata = Uri.EscapeDataString(SecurityUtility.EncryptUserData(data.UserData));
			string input = string.Format(
				"{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}",
				data.UserID,
				data.Expiration,
				userdata,
				data.Remember,
				data.UserPhone,
				data.UserName,
				data.Agent,
				JsonConvert.SerializeObject(data.Roles),
				data.LoginAccount);
			string key = SecurityUtility.EncryptSecurityKey(input);
			return Uri.EscapeDataString(key);
		}

		public static string RefeshSecurityKey(SecurityData securityData)
        {
            securityData.Expiration = SecurityUtility.GetExpiration(securityData.Remember);
            return SecurityUtility.GetSecurityKey(securityData);
        }

        public static SecurityData GetSecurityData(string securityKey)
        {
            if (string.IsNullOrWhiteSpace(securityKey)) return null;
            securityKey = Uri.UnescapeDataString(securityKey);
            string userInfo = SecurityUtility.DecryptSecurityKey(securityKey);
            string[] arr = userInfo.Split('#');

            SecurityData data = null;
            if (userInfo != null && arr.Length >= 4)
            {
                data = new SecurityData();
                DateTime expiration = DateTime.MinValue; int userId = 0; bool remember = false;
                int.TryParse(arr[0], out userId);
                DateTime.TryParse(arr[1], out expiration);
                bool.TryParse(arr[3], out remember);
                data.UserID = userId;
                data.Expiration = expiration;
                data.UserData = SecurityUtility.DecryptUserData(Uri.UnescapeDataString(arr[2]));
                data.Remember = remember;
                data.UserPhone = arr.Length >= 5 ? arr[4] : "";
                data.UserName = arr.Length >= 6 ? arr[5] : "";
            }
            return data;
        }
		/// <summary>
		/// BDapp-GetBdSecurityData
		/// </summary>
		/// <param name="securityKey"></param>
		/// <returns></returns>
		public static BdSecurityData GetBdSecurityData(string securityKey) {
			if (string.IsNullOrWhiteSpace(securityKey)) return null;

			securityKey = Uri.UnescapeDataString(securityKey);
			string userInfo = SecurityUtility.DecryptSecurityKey(securityKey);
			string[] arr = userInfo.Split('#');

			BdSecurityData data = null;
			if (userInfo != null && arr.Length >= 4) {
				data = new BdSecurityData();
				DateTime expiration = DateTime.MinValue; int userId = 0; bool remember = false;
				int.TryParse(arr[0], out userId);
				DateTime.TryParse(arr[1], out expiration);
				bool.TryParse(arr[3], out remember);
				data.UserID = userId;
				data.Expiration = expiration;
				data.UserData = SecurityUtility.DecryptUserData(Uri.UnescapeDataString(arr[2]));
				data.Remember = remember;
				data.UserPhone = arr.Length >= 5 ? arr[4] : "";
				data.UserName = arr.Length >= 6 ? arr[5] : "";
				data.Agent = arr.Length >= 7 ? arr[6] : "";
				data.Roles = arr.Length >= 8 ? JsonConvert.DeserializeObject<List<int>>(arr[7]) : new List<int>();
				data.LoginAccount = arr.Length >= 9 ? arr[8] : "";
			}
			return data;
		}

		public static DateTime GetExpiration(bool remember, int minutes = 30)
        {
            return remember ? DateTime.Now.AddDays(14) : DateTime.Now.AddMinutes(minutes);
        }


    }

    public class SecurityData
    {
        public int UserID { set; get; }
        public DateTime Expiration { set; get; }
        public string UserData { set; get; }
        public bool Remember { set; get; }
        public string UserPhone { set; get; }
        public string UserName { set; get; }
    }
	public class BdSecurityData: SecurityData {
		public string Agent { get; set; }
		public List<int> Roles { get; set; }
		public string LoginAccount { get; set; }
	}
}
