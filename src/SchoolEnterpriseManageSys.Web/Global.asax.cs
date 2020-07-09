using System;
using System.Web;
using System.Web.Mvc;
using Abp.Castle.Logging.Log4Net;
using Abp.Timing;
using Abp.Web;
using Castle.Facilities.Logging;

namespace SchoolEnterpriseManageSys.Web
{
    public class MvcApplication : AbpWebApplication<SchoolEnterpriseManageSysWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            Clock.Provider = ClockProviders.Utc;

            //屏蔽瀏覽器中的ASP.NET版本
            MvcHandler.DisableMvcResponseHeader = true;

            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                            f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
                        );

            base.Application_Start(sender, e);
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app != null && app.Context != null)
            {
                //移除Server
                app.Context.Response.Headers.Remove("Server");

                //修改Server的值
                //app.Context.Response.Headers.Set("Server", "EedcServer");

                //移除X-AspNet-Version
                app.Context.Response.Headers.Remove("X-AspNet-Version");

                //移除X-AspNetMvc-Version
                app.Context.Response.Headers.Remove("X-AspNetMvc-Version");
            }
        }

    }
}
