using System.Web.Mvc;

namespace SchoolEnterpriseManageSys.Web.Controllers
{
    public class HomeController : SchoolEnterpriseManageSysControllerBase
    {
        public ActionResult Index()
        {
            //return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
            return View();
        }
    }
}