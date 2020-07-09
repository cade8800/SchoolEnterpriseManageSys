using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace SchoolEnterpriseManageSys
{
    [DependsOn(typeof(AbpWebApiModule), typeof(SchoolEnterpriseManageSysApplicationModule))]
    public class SchoolEnterpriseManageSysWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(SchoolEnterpriseManageSysApplicationModule).Assembly, "app")
                .ForMethods(build =>
                {
                    if (build.Method.IsDefined(typeof(IgnoreWebApiAttribute)))
                    {
                        build.DontCreate = true;
                    }
                })
                .WithConventionalVerbs()//根据方法名使用惯例HTTP动词，默认对于所有的action使用Post
                .Build();
        }
    }
}
