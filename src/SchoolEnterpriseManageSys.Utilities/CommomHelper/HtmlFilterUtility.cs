using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class HtmlFilterUtility
    {
        private HtmlFilterUtility() { }

        public static readonly HtmlFilterUtility HtmlFilterInstance = new HtmlFilterUtility();


        public string FilterHtml(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return string.Empty;
            }

            string[] aryReg =
            {
              @"<script[^>]*?>.*?</script>",
              @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", @"([\r\n])[\s]+", 
              @"&(quot|#34);", @"&(amp|#38);", @"&(lt|#60);", @"&(gt|#62);", 
              @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);",
              @"&(copy|#169);", @"&#(\d+);", @"-->", @"<!--.*\n"
            };

            string[] aryRep =
            {
              "", "", "", "\"", "&", "<", ">", "   ", "\xa1",
              "\xa2",
              "\xa3", 
              "\xa9",
              "", "\r\n", ""
            };

            var newReg = aryReg[0];
            var strOutput = html;
            for (var i = 0; i < aryReg.Length; i++)
            {
                var regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput = strOutput.Replace("<", "");
            strOutput = strOutput.Replace(">", "");
            strOutput = strOutput.Replace("\r\n", "");
            return strOutput;
        }

        public string NoScript(string html)
        {
            string aryReg = @"<script[^>]*?>.*?</script>";
            var strOutput = html;
            var regex = new Regex(aryReg, RegexOptions.IgnoreCase);
            strOutput = regex.Replace(strOutput, "");
            return strOutput;
        }

    }
}
