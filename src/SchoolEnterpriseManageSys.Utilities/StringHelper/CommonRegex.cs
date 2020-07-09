using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.StringHelper
{
    public class CommonRegex
    {
        public static Regex PhoneRegex= new Regex("^[1][3,4,5,7,8]\\d{9}$");
        public static Regex PhoneRegex2= new Regex(@"^(13|14|15|16|17|18|19)\d{9}$");//tyl
        public static Regex NumberRegex = new Regex("^\\d+$");
        public static Regex EmailRegex = new Regex("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,4})+$", RegexOptions.IgnoreCase);
        public static Regex ChineseRegex=new Regex("[\u4e00-\u9fa5]+");
    }
}
