using Abp.Web.Mvc.Controllers;

namespace SchoolEnterpriseManageSys.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class SchoolEnterpriseManageSysControllerBase : AbpController
    {
        protected SchoolEnterpriseManageSysControllerBase()
        {
            LocalizationSourceName = SchoolEnterpriseManageSysConsts.LocalizationSourceName;
        }
    }
}