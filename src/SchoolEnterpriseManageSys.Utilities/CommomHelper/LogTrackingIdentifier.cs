using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper {
    public static class LogTrackingIdentifier {

        public static String GetHeaderForLog() {

            if(HttpContext.Current == null || HttpContext.Current.Request == null) return string.Empty;

            return HttpContext.Current.Request.Headers["LogTrackingIdentifier"];

        }

        public static Boolean SetHeaderForLog() {

            if(HttpContext.Current == null || HttpContext.Current.Request == null) return false;

            HttpContext.Current.Request.Headers["LogTrackingIdentifier"] = Guid.NewGuid().ToString();

            return true;
        }
    }
}
