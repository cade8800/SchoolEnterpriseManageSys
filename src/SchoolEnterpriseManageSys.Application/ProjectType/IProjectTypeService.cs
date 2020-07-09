using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using SchoolEnterpriseManageSys.ProjectType.Dto;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.ProjectType
{
    public interface IProjectTypeService : IApplicationService
    {
        [Authorize]
        GetProjectTypeOutput Get();

        [Authorize]
        void Update(ProjectTypeDto input);

        [Authorize]
        ProjectTypeDto GetDetail([Required]Enum.ProjectType? type);
    }
}
