using Abp.Application.Services;
using SchoolEnterpriseManageSys.Menu.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.Menu
{
    public interface IMenuService : IApplicationService
    {
        [Authorize]
        GetAppDateOutput GetAppDate();
    }
}
