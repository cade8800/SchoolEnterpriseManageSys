using Abp.Application.Services;
using Abp.Web.Models;
using SchoolEnterpriseManageSys.Advisory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.Advisory
{
    public interface IAdvisoryService : IApplicationService
    {
        [Authorize]
        GetAdvisoryOutput Get(GetAdvisoryInput input);

        [Authorize]
        void Insert(InsertAdvisoryInput input);

        [Authorize]
        GetEnterpriseAdvisoryOutput GetEnterpriseAdvisory(GetEnterpriseAdvisoryInput input);
    }
}
