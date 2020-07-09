using System.Reflection;
using Abp.Modules;

namespace SchoolEnterpriseManageSys
{
    public class SchoolEnterpriseManageSysCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
