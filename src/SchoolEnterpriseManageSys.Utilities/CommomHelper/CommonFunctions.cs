/* ==============================================================================
 * 功能描述：CommonFunctions  
 * 创 建 者：蒲奎民
 * 创建日期：2016-06-20 10:40:53
 * CLR Version :4.0.30319.42000
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Utilities.ExtensionHelper;
using SchoolEnterpriseManageSys.Utilities.ExceptionHeper;
using SchoolEnterpriseManageSys.Utilities.StringHelper;
namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class CommonFunctions
    {
        public CommonFunctions() { }

        #region 发送邮件方法1
        /// <summary>
        /// 发送邮件方法1
        /// </summary>
        /// <param name="title1">标题</param>
        /// <param name="title2">子标题</param>
        /// <param name="innerTable">表格HTML代码</param>
        /// <param name="to">接收人</param>
        /// <param name="ccList">抄送列表，多个用英文逗号分隔</param>
        public static void SendEmail1(string title1, string title2, string innerTable, string to, string ccList, string titleother)
        {
            //string head = "<tr><td style='width:110px'>订单ID</td><td style='width:110px'>订单明细</td><td style='width:100px'>单据</td><td style='width:180px'>重复类型</td><td style='width:180px'>实际生成时间</td><td>金额</td></tr>";
            //sbRepeatBuilder.Insert(0, "<table>" + head).Append("</table>");
            string body = CommonEmailTemplate1.Replace("[title1]", title1);
            body = body.Replace("[title2]", title2);
            body = body.Replace("[titleother]", titleother);
            body = body.Replace("[innertable]", innerTable);
            //string emails = ConfigurationUtility.AppSetting("CheckRepeatReceiptsEmails");
            //string[] emailtemp = emails.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //if (emailtemp.Length == 2)
            //{
            //    to = emailtemp[0];
            //    ccList = emailtemp[1];
            //}
            EmailModel mail = new EmailModel() { Title = title1, To = to, CCList = ccList, Body = body };
            MailHelper.Send(mail);
        } 
        #endregion

        #region 邮件发送模版1
        /// <summary>
        /// 邮件发送模版1
        /// </summary>
        public static string CommonEmailTemplate1
        {
            get
            {
                return @"<table align='center' bgcolor='#f2f2f2' border='0' cellpadding='0' cellspacing='0' width='100%'>
<tbody><tr>
<td>

<table align='center' border='0' cellpadding='0' cellspacing='0' width='870' height='200'>
<tbody><tr>
    <td style='background-color: #5C3399; color: white; padding-left: 8px;'>
        <h1>[title1]</h1>
        <h3>[title2]</h3>
        [titleother]
    </td>
</tr>
</tbody></table>
<table align='center' bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='870'>
<tbody><tr><td colspan='3' height='19'> </td></tr>
<tr>
<td width='8'></td>
    <td style='font-size: 18px; color: #343333; font-family: '微软雅黑', '宋体'; line-height: 30px; padding-top: 5px; padding-bottom: 13px' bgcolor='#f7f7f7'>
        [innertable]
    </td>
<td width='8'> </td>
</tr>
<tr><td colspan='3' height='20'></td></tr>
</tbody></table>

<table align='center' bgcolor='#282828' border='0' cellpadding='0' cellspacing='0' height='48' width='870'>
<tbody><tr><td style='font-size:12px; color:#ffffff; text-align:center; font-family:'微软雅黑','宋体';' align='center'>Copyright ? <span style='border-bottom:1px dashed #ccc;z-index:1' t='6' onclick='return false;' data='1999-2014'>2009-2016</span>, yaochufa.com, All Rights Reserved</td></tr>
<tr align='center'><td style='font-size:12px; color:#ffffff; font-family:'微软雅黑',宋体,Arial,sans-serif;'>如果您不想再收到该邮件，我也不知道怎么弄！</td>
</tr>
</tbody></table>
</td>
</tr>
</tbody></table>";
            }
        } 
        #endregion

    }
}
