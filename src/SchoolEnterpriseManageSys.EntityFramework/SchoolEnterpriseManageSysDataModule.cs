using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using SchoolEnterpriseManageSys.EntityFramework;

namespace SchoolEnterpriseManageSys
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(SchoolEnterpriseManageSysCoreModule))]
    public class SchoolEnterpriseManageSysDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<SchoolEnterpriseManageSysDbContext>(null);
        }
    }
}
