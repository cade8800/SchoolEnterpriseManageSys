using AutoMapper;
using SchoolEnterpriseManageSys.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(AutoMapperConfig), "Config")]
namespace SchoolEnterpriseManageSys.Web
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            //Mapper.AssertConfigurationIsValid();//验证所有的映射配置是否都正常
        }
    }
}