using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Kulv.Sems.API.MessageHandler;

namespace SchoolEnterpriseManageSys.Utilities.DEncryptHelper
{
    public class EncryptUtility
    {
        public static string Md5(string str)
        {
            return Md5Base(str, Encoding.UTF8);
        }

        public static string Md5Base(string str, Encoding encoding)
        {
            try
            {
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    byte[] hashvalue = md5.ComputeHash(encoding.GetBytes(str));
                    return BitConverter.ToString(hashvalue).Replace("-", "");
                }
            }
            catch
            {
                return String.Empty;
            }
        }

        //hex repr. of md5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format">format value x or x2</param>
        /// <returns></returns>
        public static string Md5Hex(string str, string format = "x2")
        {
            return Md5Hex(str, Encoding.UTF8, format);
        }

        //hex repr. of md5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format">format value x or x2</param>
        /// <returns></returns>
        public static string Md5Hex(string str, Encoding encoding, string format = "x2")
        {
            try
            {
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    byte[] hashvalue = md5.ComputeHash(encoding.GetBytes(str));
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashvalue)
                    {
                        //{0:x2} or {0:x}
                        var f = "{0:" + format + "}";
                        sb.AppendFormat(f, b);
                    }
                    return sb.ToString();
                }
            }
            catch
            {
                return String.Empty;
            }
        }

        #region 加密、解密

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="origin">待加密的字符串</param>
        /// <param name="key">加密密钥,要求为8位，超出长度则截取前8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返空字符串</returns>
        public static string EncryptDES(string origin, string key)
        {
            if (string.IsNullOrEmpty(key) || key.Length < 8)
            {
                return string.Empty;
            }

            try
            {
                byte[] bOrigin = Encoding.UTF8.GetBytes(origin);
                byte[] bKey = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 8));
                byte[] bIV = null;
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                desc.Padding = PaddingMode.PKCS7;
                desc.Mode = CipherMode.ECB;
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(bOrigin, 0, bOrigin.Length);
                        cStream.FlushFinalBlock();
                        return BytesToHexStr(mStream.ToArray());
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="encr">待解密的字符串</param>
        /// <param name="key">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返空字符串</returns>
        public static string DecryptDES(string encr, string key)
        {
            if (string.IsNullOrEmpty(key) || key.Length < 8)
            {
                return string.Empty;
            }

            try
            {
                byte[] bOrigin = HexStrToBytes(encr);
                byte[] bKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] bIV = null;
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                DCSP.Mode = CipherMode.ECB;
                DCSP.Padding = PaddingMode.PKCS7;
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(bOrigin, 0, bOrigin.Length);
                        cStream.FlushFinalBlock();
                        return Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }


        //private static byte[] DESKeys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// DES加密 by IV
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <param name="desKeys"></param>
        /// <returns></returns>
        public static string DESEncode(string encryptString, string encryptKey, byte[] desKeys)
        {
            //encryptKey = encryptKey.Substring(0,8); 
            //encryptKey = encryptKey.PadRight(8, ' '); 
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = desKeys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            using (DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider())
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (var encryptor = dCSP.CreateEncryptor(rgbKey, rgbIV))
                    {
                        using (CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
                        {
                            cStream.Write(inputByteArray, 0, inputByteArray.Length);
                            cStream.FlushFinalBlock();
                        }
                    }

                    return Convert.ToBase64String(mStream.ToArray());
                }
            }
        }

        /// <summary>
        /// DES解密 by IV
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string DESDecode(string decryptString, string decryptKey, byte[] desKeys)
        {
            try
            {
                //decryptKey = decryptKey.SubString(8, ""); 
                //decryptKey = decryptKey.PadRight(8, ' '); 
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = desKeys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV),
               CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="origin">待编码的字符串</param>
        /// <returns>成功则返回编码后的字符串，失败则返回空字符串</returns>
        public static string EncodeBase64(string origin)
        {
            string encoded = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(origin);
            try
            {
                encoded = Convert.ToBase64String(bytes);
            }
            catch
            { }
            return encoded;
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="encoded">待解码的字符串</param>
        /// <returns>成功则返回解码后的字符串，失败则返回空字符串</returns>
        public static string DecodeBase64(string encoded)
        {
            string origin = string.Empty;
            byte[] bytes = Convert.FromBase64String(encoded);
            try
            {
                origin = Encoding.UTF8.GetString(bytes);
            }
            catch
            { }
            return origin;
        }

        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes">字节数组</param> 
        /// <returns>16进制字符串</returns> 
        public static string BytesToHexStr(byte[] bytes)
        {
            string result = string.Empty;
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    result += bytes[i].ToString("X2");
                }
            }
            return result;
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="hexStr">16进制字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] HexStrToBytes(string hexStr)
        {
            if ((hexStr.Length % 2) != 0)
            {
                throw new ArgumentException();
            }

            byte[] bytes = new byte[hexStr.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    bytes[i] = byte.Parse(hexStr.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                catch
                { }
            }

            return bytes;
        }

        #endregion


        #region AES
        /// <summary>
        /// AES加密 CBC模式
        /// </summary>
        /// <param name="toEncrypt">原文</param>
        /// <param name="key">AESKey</param>
        /// <returns></returns>
        public static string EncryptAES(string toEncrypt, string key)
        {
            string iv = key;
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str">要加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static byte[] EncryptAESBytes(string str, string strAesKey)
        {
            /*   Byte[] keyArray = hexStringToByte(strAesKey);//System.Text.UTF8Encoding.UTF8.GetBytes(strAesKey);
               Byte[] toEncryptArray = hexStringToByte(hex);//System.Text.UTF8Encoding.UTF8.GetBytes(hex);
            
               System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
               rDel.Key = keyArray;
               rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
               //rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
               rDel.KeySize = 128;
               rDel.BlockSize = 128;
               System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateEncryptor();
               Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

               return resultArray;
             */
            byte[] bKey = hexStringToByte(strAesKey);
            byte[] ptext = Encoding.UTF8.GetBytes(str);
            Rijndael RijndaelAlg = Rijndael.Create();
            RijndaelAlg.Mode = CipherMode.ECB;
            RijndaelAlg.KeySize = 128;
            RijndaelAlg.BlockSize = 128;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, RijndaelAlg.CreateEncryptor(bKey, null), CryptoStreamMode.Write);
            cs.Write(ptext, 0, ptext.Length);
            cs.FlushFinalBlock();
            ms.Close();
            return ms.ToArray();
        }

        public static byte[] hexStringToByte(string hex)
        {
            int len = hex.Length;
            if (len % 2 != 0)
            {
                hex = "0" + hex;
                len++;
            }
            len /= 2;
            byte[] result = new byte[len];
            char[] achar = hex.ToUpper().ToCharArray();
            for (int i = 0; i < len; i++)
            {
                int pos = i * 2;
                result[i] = (byte)((int)toByte(achar[pos]) << 4 | (int)toByte(achar[pos + 1]));
            }
            return result;
        }

        public static string bytesToHexString(byte[] bArray)
        {
            StringBuilder sb = new StringBuilder(bArray.Length);
            for (int i = 0; i < bArray.Length; i++)
            {
                string sTemp = fillToHexString(Convert.ToString((int)(255 & bArray[i]), 16));
                sb.Append(sTemp.ToLower());
            }
            return sb.ToString();
        }

        public static string fillToHexString(string inputStr)
        {
            return fillString(inputStr, 2, '0');
        }

        public static string fillString(string str, int length, char c)
        {
            if (str == null || str.Length > length)
            {
                throw new Exception();
            }
            int a = length - str.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < a; i++)
            {
                sb.Append(c);
            }
            sb.Append(str);
            return sb.ToString();
        }

        private static byte toByte(char c)
        {
            return (byte)"0123456789ABCDEF".IndexOf(c);
        }

        /// <summary>
        /// AES加密 返回16进制字符串
        /// </summary>
        /// <param name="str">要加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static String EncryptAESHex(string str, string strAesKey)
        {
            try
            {
                Byte[] resultArray = EncryptAESBytes(str, strAesKey);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in resultArray)
                    sb.AppendFormat("{0:x2}", b);
                string result = sb.ToString();
                return bytesToHexString(resultArray);
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str">要解密字符串</param>
        /// <returns>返回解密后字符串</returns>
        public static string DecryptAES(string str, string strAesKey)
        {
            var bArray = hexStringToByte(str);

            byte[] bKey = hexStringToByte(strAesKey);
            Rijndael RijndaelAlg = Rijndael.Create();
            RijndaelAlg.Mode = CipherMode.ECB;
            RijndaelAlg.KeySize = 128;
            RijndaelAlg.BlockSize = 128;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, RijndaelAlg.CreateDecryptor(bKey, null), CryptoStreamMode.Write);
            cs.Write(bArray, 0, bArray.Length);
            cs.FlushFinalBlock();
            ms.Close();

            return Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// AES解密(可适用js AES加密的解密)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password">16位字符</param>
        /// <param name="iv">16位字符</param>
        /// <returns></returns>
        public static string DecryptAES(string text, string password, string iv)
        {
            using (var rijndaelCipher = new RijndaelManaged())
            {
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;
                var encryptedData = Convert.FromBase64String(text);
                var pwdBytes = Encoding.UTF8.GetBytes(password);
                var keyBytes = new byte[16];
                var len = pwdBytes.Length;
                if (len > keyBytes.Length) len = keyBytes.Length;
                Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;
                var ivBytes = Encoding.UTF8.GetBytes(iv);
                rijndaelCipher.IV = ivBytes;
                using (var transform = rijndaelCipher.CreateDecryptor())
                {
                    var plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    return Encoding.UTF8.GetString(plainText);
                }
            }
        }

        #endregion

        /// <summary>
        /// 加密短链接
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="salt">salt</param>
        /// <returns></returns>
        public static string EncryptShorUrl(string url, string salt)
        {
            // 不固定 php会出现 1004 和 内部错误
            string time = "20150906220512";
            string md5Str = EncryptUtility.Md5(String.Format("{{\"url\":\"{0}\"}}###{1}###{2}", url, time, salt));
            return EncryptionUtils.Encrypt3DES(String.Format("{{\"url\":\"{0}\"}}###{1}###{2}", url, time, md5Str.ToLower()));
        }

        #region 3DES base64
        /// <summary>
        /// 3DES数据加密，BASE64编码
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Key = UTF8Encoding.UTF8.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;
            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = UTF8Encoding.UTF8.GetBytes(a_strString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }


        /// <summary>
        /// 3DES数据解密，BASE64编码
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Key = UTF8Encoding.UTF8.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            string result = ""; try
            {
                byte[] Buffer = Convert.FromBase64String(a_strString);
                result = UTF8Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch// (Exception e)
            { }
            return result;
        }
        #endregion
    }
}
