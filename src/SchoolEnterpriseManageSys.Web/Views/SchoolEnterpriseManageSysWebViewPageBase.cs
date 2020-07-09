using Abp.Web.Mvc.Views;

namespace SchoolEnterpriseManageSys.Web.Views
{
    public abstract class SchoolEnterpriseManageSysWebViewPageBase : SchoolEnterpriseManageSysWebViewPageBase<dynamic>
    {

    }

    public abstract class SchoolEnterpriseManageSysWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SchoolEnterpriseManageSysWebViewPageBase()
        {
            LocalizationSourceName = SchoolEnterpriseManageSysConsts.LocalizationSourceName;
        }
    }
}