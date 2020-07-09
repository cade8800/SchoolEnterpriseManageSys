using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Linq;

namespace SchoolEnterpriseManageSys.Utilities.DEncryptHelper
{
    /// <summary>
    /// 加密字符串辅助类
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// MD5 hash加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5ByHash(string str)
        {
            if (str == null)
            {
                throw new ArgumentException("加密内容为NULL", "str");
            }

            using (var md5 = new MD5CryptoServiceProvider())
            {
                var result = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str.Trim())));
                return result;
            }
        }
        /// <summary>
        /// MD5 hash加密（已替换 -）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string MD5ByHash(string str, Encoding encoding)
        {
            if (str == null)
            {
                throw new ArgumentException("加密内容为NULL", "str");
            }

            encoding = encoding ?? Encoding.UTF8;
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var result = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(str.Trim())));
                return result.Replace("-", "");
            }
        }
        /// <summary>
        /// net 自带MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }

        /// <summary>
        /// 常规版本的MD5
        /// </summary>
        public static string MD5_Noraml(string str)
        {
            return Encrypt.MD5ByHash(str, Encoding.UTF8);
            //return new System.Security.Cryptography.MD5CryptoServiceProvider()
            //                .ComputeHash(Encoding.UTF8.GetBytes(str))
            //                .Aggregate("", (s, n) => s += Convert.ToString(n, 16).PadLeft(2, '0').ToUpper());
        }

        #region DES加密解密
        //默认密钥向量
        private static readonly byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        public static string DESEncrypt(string pToEncrypt)
        {
            var encryptKey = ConfigurationManager.AppSettings["DesKey"];
            using (var des = new DESCryptoServiceProvider())
            {
                var inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                des.Key = Encoding.ASCII.GetBytes(encryptKey);
                des.IV = Encoding.ASCII.GetBytes(encryptKey);
                var ret = new StringBuilder("");
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }
                    
                    foreach (var b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:X2}", b);
                    }
                }

                return ret.ToString();
            }
        }



        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <returns></returns>
        public static string DESDecrypt(string pToDecrypt)
        {
            try
            {
                var encryptKey = ConfigurationManager.AppSettings["DesKey"];
                using (var des = new DESCryptoServiceProvider())
                {
                    var inputByteArray = new byte[pToDecrypt.Length/2];
                    for (var x = 0; x < pToDecrypt.Length/2; x++)
                    {
                        var i = (Convert.ToInt32(pToDecrypt.Substring(x*2, 2), 16));
                        inputByteArray[x] = (byte) i;
                    }

                    des.Key = Encoding.ASCII.GetBytes(encryptKey);
                    des.IV = Encoding.ASCII.GetBytes(encryptKey);
                    var ms = new MemoryStream();
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }

                    var r = Encoding.Default.GetString(ms.ToArray());
                    ms.Close();

                    return r;
                }

            }
            catch
            {
                return null;
            }
        }

        public static string ToBase64String(string value)
        {
            string objectMeta;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, value);
                objectMeta = Convert.ToBase64String(ms.ToArray());
            }

            return objectMeta;
        }


        #region ========加密========

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DESEncrypt(string input, string sKey, string sIv)
        {
            if (string.IsNullOrEmpty(input))
                return String.Empty;
            var key = Encoding.UTF8.GetBytes(sKey);
            var iv = Encoding.UTF8.GetBytes(sIv);
            using (var des = new DESCryptoServiceProvider())
            {
                using (var ms = new MemoryStream())
                {
                    using (var encStream = new CryptoStream(ms, des.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(encStream))
                        {
                            sw.Write(input);
                            sw.Flush();
                            encStream.FlushFinalBlock();
                            ms.Flush();
                            var result = Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length, System.Globalization.CultureInfo.InvariantCulture));
                            //var result= Encoding.UTF8.GetString(ms.GetBuffer());
                            return result;
                        }
                    }
                }
            }
        }

        #endregion

        #region ========解密========
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DESDecrypt(string input, string sKey, string sIv)
        {
            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(input);
            }
            catch (System.ArgumentNullException)
            {
                return String.Empty;
            }
            catch (System.FormatException)
            {
                return String.Empty;
            }
            var key = Encoding.UTF8.GetBytes(sKey);
            var iv = Encoding.UTF8.GetBytes(sIv);
            using (var des = new DESCryptoServiceProvider())
            {
                using (var ms = new MemoryStream(buffer))
                {
                    using (var encStream = new CryptoStream(ms, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(encStream))
                        {
                            var result = sr.ReadToEnd();
                            return result;
                        }
                    }
                }
            }

        }
        #endregion

        ///// <summary>
        ///// DES加密字符串
        ///// </summary>
        ///// <param name="encryptString">待加密的字符串</param>
        ///// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        //public static string Encode(string encryptString)
        //{
        //    var encryptKey = ConfigurationManager.AppSettings["DesKey"];
        //    encryptKey = encryptKey.Substring(0, 8);
        //    encryptKey = encryptKey.PadRight(8, ' ');
        //    var rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
        //    var rgbIv = Keys;
        //    var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
        //    var dCsp = new DESCryptoServiceProvider();
        //    var mStream = new MemoryStream();
        //    var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
        //    cStream.Write(inputByteArray, 0, inputByteArray.Length);
        //    cStream.FlushFinalBlock();
        //    return Convert.ToBase64String(mStream.ToArray());

        //}

        ///// <summary>
        ///// DES解密字符串
        ///// </summary>
        ///// <param name="decryptString">待解密的字符串</param>
        ///// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        //public static string Decode(string decryptString)
        //{
        //    try
        //    {
        //        var decryptKey = ConfigurationManager.AppSettings["DesKey"];
        //        decryptKey = decryptKey.Substring(0, 8);
        //        decryptKey = decryptKey.PadRight(8, ' ');
        //        var rgbKey = Encoding.UTF8.GetBytes(decryptKey);
        //        var rgbIv = Keys;
        //        var inputByteArray = Convert.FromBase64String(decryptString);
        //        var dcsp = new DESCryptoServiceProvider();

        //        var mStream = new MemoryStream();
        //        var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
        //        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        //        cStream.FlushFinalBlock();
        //        return Encoding.UTF8.GetString(mStream.ToArray());
        //    }
        //    catch
        //    {
        //        return "";
        //    }

        //}
        #endregion

        #region  HMACSHA1加密并转Base64
        /// <summary>
        /// HMACSHA1加密并转Base64(如果data或key为空，返回"")
        /// </summary>
        /// <param name="data">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>按照密钥加密后并转Base64的结果</returns>
        public static string HMACSHA1EncryptAndBase64(string data, string key)
        {
            string result = "";
            if (!string.IsNullOrWhiteSpace(data) && !string.IsNullOrWhiteSpace(key))
            {
                Encoding encode = Encoding.UTF8;
                byte[] byteData = encode.GetBytes(data);
                byte[] byteKey = encode.GetBytes(key);
                HMACSHA1 hmac = new HMACSHA1(byteKey);
                using (CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write))
                {
                    cs.Write(byteData, 0, byteData.Length);
                }
                result = Convert.ToBase64String(hmac.Hash);
            }
            return result;
        }
        #endregion

    }
}
