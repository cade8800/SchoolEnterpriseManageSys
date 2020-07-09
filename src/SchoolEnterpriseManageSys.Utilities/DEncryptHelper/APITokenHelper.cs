using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Utilities.CachingGHelper;

namespace SchoolEnterpriseManageSys.Utilities.DEncryptHelper
{
    public class APITokenHelper
    {
        static ILog logger = LogManager.GetLogger(typeof(APITokenHelper));

        private static TimeSpan timeOut = new TimeSpan(0, 1, 0);

        private static APICryptoResult ValidToken(Func<string, string> hash, Func<string, string, string> decrypt, string securityKey, string token)
        {
            var result = APICrypto.Decrypt(hash, decrypt, securityKey, token);

            if (result.Success)
            {
                var clientTime = new DateTime(result.Timestamp);
                //防止重放请求,防止服务器时间不一致
                if (DateTime.Now.Subtract(clientTime) > timeOut)
                {
                    result.Success = false;
                    result.Message = "该请求已过期";
                    return result;
                }
                else
                {
                    if (Memcached.AddCacheExplicit(result.Id, DateTime.Now.ToString("G"), (int)timeOut.TotalMinutes))
                    {
                        return result;
                    }
                    else
                    {
                        if (Memcached.GetCache(result.Id) != null)
                        {
                            //如果已存在KEY,则为重放请求
                            result.Success = false;
                            result.Message = "token已过期";
                            return result;
                        }
                        else
                        {
                            logger.Info("缓存服务异常，身份验证已放行");
                            //缓存服务异常
                            return result;
                        }
                    }
                }
            }
            else  //签名不合法
            {
                result.Success = false;
                result.Message = "无效的token";
                return result;
            }
        }


        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="securityKey">密钥</param>
        /// <param name="authInfo">附加信息</param>
        public static APICryptoResult Generate(string securityKey, string authInfo)
        {
            return APICrypto.Encrypt(securityKey, authInfo);
        }

        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="token">token字符串</param>
        /// <param name="securityKey">密钥</param>
        /// <returns>Item1:验证成功 or 失败, Item2: 附加信息 or 失败信息</returns>
        public static APICryptoResult Valid(string token, string securityKey, Action<Exception> ExceptionHandler = null)
        {
            try
            {
                return ValidToken(EncryptUtility.Md5, AESCrypto.Decrypt, securityKey, token);
            }
            catch (Exception ex)
            {
                if (ExceptionHandler != null)
                    ExceptionHandler(ex);
                return new APICryptoResult
                {
                    Success = false,
                    Message = "验证异常"
                };
            }
        }
    }
}
