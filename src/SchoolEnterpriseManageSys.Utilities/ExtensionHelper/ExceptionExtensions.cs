using SchoolEnterpriseManageSys.Utilities.ExceptionHeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 从异常信息判断单据表插入异常是否因为ReceiptNo重复
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool IsReceiptsNoRepeat(this Exception ex)
        {
            if (ex.Message == null) return false;
            string message = new ExceptionMessage(ex).ToString().ToLowerInvariant();
            return (message.Contains("dbo.financereceipts") && message.Contains("unique index") && message.Contains("key") && message.Contains("receiptno"));
        }
        /// <summary>
        /// 从异常信息判断单据表插入异常是否为超时异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool IsTimeoutException(this Exception ex)
        {
            if (ex.Message == null) return false;
            string message = ex.Message.ToLowerInvariant();//Timeout 时间已到。在操作完成之前超时时间已过或服务器未响应。
            return (message.Contains("Timeout") && message.Contains("超时时间已过") && message.Contains("时间已到"));
        }

        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fun">
        ///  无参数，返回object类型的方法
        ///   () =>
        ///    {
        ///    int a = 3, b = 0;//自定义代码
        ///    return a / b;//随便返回一个
        ///    }
        ///    </param>
        /// <param name="funCondition">
        /// 参数Exception类型，返回bool类型的方法
        ///    (ex) =>
        ///    {
        ///        return ex.IsReceiptsNoRepeat();
        ///    }
        /// </param>
        /// <param name="MessageToShow">出错时抛的异常内容</param>
        public static void TryCatch3(Func<object> fun, Func<Exception, bool> funCondition, string MessageToShow = "")
        {
            try
            {
                fun();
            }
            catch (Exception ex)
            {
                if (funCondition(ex))
                {
                    try
                    {
                        fun();
                    }
                    catch (Exception ex2)
                    {
                        if (funCondition(ex2))
                        {
                            try
                            {
                                fun();
                            }
                            catch (Exception ex3)
                            {
                                if (MessageToShow != "") throw new BusinessException(MessageToShow);
                                else throw new BusinessException(ex3.Message, ex3);
                            }
                        }
                        else if (MessageToShow != "")
                        {
                            throw new BusinessException(MessageToShow);
                        }
                    }
                }
                else if (MessageToShow != "")
                {
                    throw new BusinessException(MessageToShow);
                }
            }
        }

        /// <summary>
        /// 检查是否非空，如果对象为空，抛出BusinessException
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="variableDescription"></param>
        /// <param name="extraMessage">可记录额外的与业务相关的信息</param>
        public static void CheckNull(this object obj, string variableDescription,string extraMessage="")
        {
            if (obj == null) throw new BusinessException(string.Format("对象:{0}值为空 {1}", variableDescription, extraMessage));
            if (obj is string && obj.ToString() == "") throw new BusinessException(string.Format("字符串对象:{0}值为空 {1}", variableDescription,extraMessage));
        }
    }
}
