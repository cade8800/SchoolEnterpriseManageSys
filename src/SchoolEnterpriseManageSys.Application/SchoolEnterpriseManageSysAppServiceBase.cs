using Abp.Application.Services;

namespace SchoolEnterpriseManageSys
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class SchoolEnterpriseManageSysAppServiceBase : ApplicationService
    {
        protected SchoolEnterpriseManageSysAppServiceBase()
        {
            LocalizationSourceName = SchoolEnterpriseManageSysConsts.LocalizationSourceName;
        }
    }
}