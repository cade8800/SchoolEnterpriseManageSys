using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.DEncryptHelper
{
    public class APICrypto
    {
        private const string Delimiter = "%$%$";
        private const string EscapeDelimiterStr = "/$/$";

        public static APICryptoResult Encrypt(string securityKey, string plainText)
        {
            return Encrypt(EncryptUtility.Md5, AESCrypto.Encrypt, securityKey, plainText);
        }
        public static APICryptoResult Decrypt(string securityKey, string ciphertext)
        {
            return Decrypt(EncryptUtility.Md5, AESCrypto.Decrypt, securityKey, ciphertext);
        }

        public static APICryptoResult Encrypt(Func<string, string> hash, Func<string, string, string> encrypt, string securityKey, string plaintext)
        {
            string requestId = Guid.NewGuid().ToString("N");
            if (plaintext == null) plaintext = string.Empty;
            EscapeDelimiter(ref plaintext);
            var now = DateTime.Now.Ticks;
            var text = requestId + Delimiter + now + Delimiter + hash(securityKey) + Delimiter + plaintext;
            return new APICryptoResult
            {
                Success = true,
                Id = requestId,
                Timestamp = now,
                Ciphertext = encrypt(text + Delimiter + hash(text), securityKey),
                Plaintext = plaintext
            };
        }

        public static APICryptoResult Decrypt(Func<string, string> hash, Func<string, string, string> decrypt, string securityKey, string ciphertext)
        {
            var decryptText = decrypt(ciphertext, securityKey);
            if (string.IsNullOrEmpty(decryptText))
                return new APICryptoResult
                {
                    Success = false,
                    Message = "密文无效"
                };

            var textArr = decryptText.Split(new[] { Delimiter }, StringSplitOptions.None);
            var requestId = textArr[0];
            var ticks = Convert.ToInt64(textArr[1]);
            var securityKeyHash = textArr[2];
            var msg = textArr[3];
            UnescapeDelimiter(ref decryptText);
            var textHash = textArr[4];

            var text = requestId + Delimiter + ticks + Delimiter + hash(securityKey) + Delimiter + msg;
            if (hash(text) == textHash)
            {
                return new APICryptoResult
                {
                    Id = requestId,
                    Timestamp = ticks,
                    Ciphertext = ciphertext,
                    Plaintext = msg,
                    Success = true
                };
            }
            else
            {
                return new APICryptoResult
                {
                    Success = false,
                    Message = "密文不合法"
                };
            }
        }

        private static void EscapeDelimiter(ref string str)
        {
            str = str.Replace(Delimiter, EscapeDelimiterStr);
        }
        private static void UnescapeDelimiter(ref string str)
        {
            str = str.Replace(EscapeDelimiterStr, Delimiter);
        }

    }
}
