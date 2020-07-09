using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SchoolEnterpriseManageSys.Enterprise.Dto;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.Enterprise
{
    public interface IEnterpriseService : IApplicationService
    {
        [Authorize]
        GetEnterpriseOutput Get(GetEnterpriseInput input);

        [Authorize]
        GetEnterpriseDetailOutput GetDetail(GetEnterpriseDetailInput input);

        [Authorize]
        void Edit(EnterpriseOutput input);
    }
}
