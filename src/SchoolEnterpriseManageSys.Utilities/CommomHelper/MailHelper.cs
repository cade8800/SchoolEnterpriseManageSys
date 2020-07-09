/* ==============================================================================
 * 功能描述：MailHelper  
 * 创 建 者：蒲奎民
 * 创建日期：2016-06-20 10:43:55
 * CLR Version :4.0.30319.42000
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class MailHelper
    {
        private static MailMessage _mailMessage;
        private static SmtpClient _smtpClient;




        /// <summary>  
        /// 处审核后类的实例  
        /// </summary>
        private static void GetMailMessageInstance(EmailModel email)
        {
            _mailMessage = new MailMessage();
            _mailMessage.To.Add(email.To);
            _mailMessage.From = new MailAddress(email.From);
            _mailMessage.Subject = email.Title;
            _mailMessage.Body = email.Body;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            _mailMessage.Priority = MailPriority.Normal;
            if (!string.IsNullOrEmpty(email.CCList))
            {
                _mailMessage.CC.Add(email.CCList);
            }
        }

        /**/
        /// <summary>  
        /// 添加附件  
        /// </summary>
        public static void Attachments(string filePath)
        {
            var path = filePath.Split(',');
            foreach (var t in path)
            {
                var data = new Attachment(t, MediaTypeNames.Application.Octet);
                var disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(t);//获取附件的创建日期
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(t);//获取附件的修改日期  
                disposition.ReadDate = System.IO.File.GetLastAccessTime(t);//获取附件的读取日期  
                _mailMessage.Attachments.Add(data);//添加到附件中  
            }
        }

        /// <summary>  
        /// 异步发送邮件
        /// </summary>
        /// <param name="email"></param>
        /// <param name="completedMethod"></param>
        public static void SendAsync(EmailModel email, SendCompletedEventHandler completedMethod)
        {
            if (email != null)
            {
                GetMailMessageInstance(email);
                if (string.IsNullOrEmpty(email.To)) throw new ArgumentNullException("To", "邮件接收人不能为空！");
                _smtpClient = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential(email.UserCode, email.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = email.Host,
                };
                _smtpClient.SendCompleted += completedMethod;//注册异步发送邮件完成时的事件 
                _smtpClient.SendAsync(_mailMessage, email);
            }
        }
        /// <summary>  
        /// 发送邮件
        /// </summary>
        public static void Send(EmailModel email)
        {
            GetMailMessageInstance(email);
            if (_mailMessage != null)
            {
                if (string.IsNullOrEmpty(email.To)) throw new ArgumentNullException("To", "邮件接收人不能为空！");
                _smtpClient = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential(email.UserCode, email.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = email.Host,
                };
                _smtpClient.Send(_mailMessage);
            }
        }

        /// <summary>  
        /// 发送邮件（含附件）
        /// </summary>
        public static void SendWithAttachments(EmailModel email, string filePath)
        {
            GetMailMessageInstance(email);
            if (_mailMessage != null)
            {
                if (string.IsNullOrEmpty(email.To)) throw new ArgumentNullException("To", "邮件接收人不能为空！");
                if (!string.IsNullOrEmpty(filePath))
                    Attachments(filePath);
                _smtpClient = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential(email.UserCode, email.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = email.Host,
                };
                _smtpClient.Send(_mailMessage);
            }
        }
    }
    public class EmailModel
    {

        public EmailModel()
        {
            To = "";
            Password = ConfigurationManager.AppSettings["EmailPassword"];
            UserCode = ConfigurationManager.AppSettings["EmailUserCode"];
            From = ConfigurationManager.AppSettings["EmailFromAddress"];
            Host = ConfigurationManager.AppSettings["EmailHost"];
            EmailFromName = ConfigurationManager.AppSettings["EmailFromName"];
        }

        /// <summary>
        ///收件人
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发送密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 发件人名称
        /// </summary>
        public string EmailFromName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// HOST
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 参数-值对应 (Json格式)
        /// </summary>
        public string ParasJson { get; set; }

        public int UserId { get; set; }
        /// <summary>
        /// 抄送列表，英文逗号分隔
        /// </summary>
        public string CCList { get; set; }
    }
}
