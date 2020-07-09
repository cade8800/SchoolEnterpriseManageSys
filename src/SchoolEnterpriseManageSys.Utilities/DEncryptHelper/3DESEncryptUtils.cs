using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Kulv.Sems.API.MessageHandler
{
    /// <summary>
    /// 3DES数据加密工具类 ---add by weifb
    /// </summary>
    public sealed class EncryptionUtils
    {
        /// <summary>
        /// Crypto3DES 的摘要说明。数据加密工具类, 做成类库对于密钥和矢量的保管比较方便
        /// </summary>
        public EncryptionUtils() { }
        /// <summary>
        /// 获取密钥key--24字节
        /// </summary>
        private static string sKey
        {
            get
            {
                //return ConfigurationManager.AppSettings["cryptoKey"];
                return "abcdefghijklmnopqrstuvwx";
            }
        }
        /// <summary>
        ///   初始向量IV--8字节
        /// </summary>
        private static string sIV
        {
            get
            {
                //return ConfigurationManager.AppSettings["cryptoIV"];
                return "12345678";
            }
        }
        /// <summary>
        ///  获取加密密盐
        /// </summary>
        public static string SecuritySalt
        {
            get
            {
                //return ConfigurationManager.AppSettings["SecuritySalt"];
                return "abc!@cd";
            }
        }
        /// <summary>
        /// 根据传入的明文json数据返回带格式的密文
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static string EncryptFormatStr(string jsonStr)
        {
            string messageFormatStr = jsonStr + "###" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string messageSecuritySaltStr = messageFormatStr + "###" + EncryptionUtils.SecuritySalt;
            jsonStr = messageFormatStr + "###" + EncryptionUtils.MD5(messageSecuritySaltStr);
            string encryptedMessage = EncryptionUtils.Encrypt3DES(jsonStr);
            return encryptedMessage;
        }
        /// <summary>
        /// 解密带格式的密文数据后返回明文json
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public static string DecryptFormatStr(string encryptStr)
        {
            var decryptContent = EncryptionUtils.Decrypt3DES(encryptStr);
            string[] paramArray = decryptContent.Split(new string[] { "###" }, StringSplitOptions.RemoveEmptyEntries);
            return paramArray[0];
        }


        private static System.Text.Encoding encoding;
        /// <summary>
        /// 获取或设置加密解密的编码
        /// </summary>
        public static System.Text.Encoding Encoding
        {
            get
            {
                if (encoding == null)
                {
                    encoding = System.Text.Encoding.UTF8;
                }
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }
        #region  加密解密
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="strString">输入的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt3DES(string strString)
        {
            //构造一个对称算法
            using (TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider())
                {
                    DES.IV = Encoding.GetBytes(sIV);
                    DES.Key = Encoding.GetBytes(sKey);
                    DES.Mode = System.Security.Cryptography.CipherMode.CBC;
                    DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                    ICryptoTransform DESEncrypt = DES.CreateEncryptor();
                    byte[] Buffer = Encoding.GetBytes(strString);
                    return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                }
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="encryptStr">加过密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt3DES(string encryptStr)
        {
            //构造一个对称算法
            using (TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider())
            {
                DES.IV = Encoding.GetBytes(sIV);
                DES.Key = Encoding.GetBytes(sKey);
                DES.Mode = System.Security.Cryptography.CipherMode.CBC;
                DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ICryptoTransform DESDecrypt = DES.CreateDecryptor();
                string result = "";
                try
                {
                    byte[] Buffer = Convert.FromBase64String(encryptStr);
                    result = Encoding.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                }
                catch (System.Exception e)
                {
                    throw (new System.Exception("参数解密失败", e));
                }
                return result;
            }
        }

        /// <summary>  
        /// MD5编码  
        /// </summary>  
        public static string MD5(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                var datas = Encoding.GetBytes(str);
                var hash = md5.ComputeHash(datas);
                str = string.Empty;
                //把MD5所得用16进制小写的字符串形式，让函数返回一个32字节长的可打印字符串。  
                for (int i = 0; i < hash.Length; i++)
                {
                    str += hash[i].ToString("X").PadLeft(2, '0');
                }
                return str;
            }
        }

        #endregion
    }
}
