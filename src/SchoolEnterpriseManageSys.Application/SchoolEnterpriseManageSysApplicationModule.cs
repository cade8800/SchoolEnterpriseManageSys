using System.Reflection;
using Abp.Modules;

namespace SchoolEnterpriseManageSys
{
    [DependsOn(typeof(SchoolEnterpriseManageSysCoreModule))]
    public class SchoolEnterpriseManageSysApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AutoMapper.Mapper.Initialize(cfg=> {
                cfg.AddProfile<MapperProfile>();
            });
        }
    }
}
