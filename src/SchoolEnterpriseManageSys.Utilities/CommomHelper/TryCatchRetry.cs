using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class TryCatchRetry
    {
        /// <summary>
        /// 重试
        /// </summary>
        /// <param name="action">重试方法体</param>
        /// <param name="retries">重试次数</param>
        /// <param name="tryConditions">重试条件</param>
        /// <param name="lastRetryCallback">最后一次重试回调</param>
        public static void Run(Action action, ref bool tryConditions, int retries = 3, Action lastRetryCallback = null)
        {
            int attempts = 0;

            while (true)
            {
                attempts++;

                try
                {
                    action.Invoke();
                    if (tryConditions)
                    {
                        break;
                    }
                    //重试最后一次
                    if (attempts == retries)
                    {
                        if (lastRetryCallback != null)
                        {
                            lastRetryCallback();
                        }
                        break;
                    }
                }
                catch (Exception exception)
                {
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    Debug.WriteLine(exception);

                    if (attempts >= retries)
                    {
                        //if (retryConditions)
                        {
                            throw;
                        }

                        //return;
                    }
                }
            }
        }



        public static void Run<T>(Action action, int retries = 1, bool throwOnFail = false) where T : Exception
        {
            int attempts = 0;

            while (true)
            {
                attempts++;

                try
                {
                    action.Invoke();
                    return;
                }
                catch (T exception)
                {
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    Debug.WriteLine(exception);

                    if (attempts > retries)
                    {
                        if (throwOnFail)
                        {
                            throw;
                        }

                        return;
                    }
                }
            }
        }
    }
}
