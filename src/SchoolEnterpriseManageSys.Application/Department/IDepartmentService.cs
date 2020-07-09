using Abp.Application.Services;
using SchoolEnterpriseManageSys.Department.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.Department
{
    public interface IDepartmentService : IApplicationService
    {
        [Authorize]
        GetDepartmentOutput Get();

        [Authorize]
        void Delete(Guid? id);

        [Authorize]
        void Update(DepartmentDto input);

        [Authorize]
        Task<Guid> Add(AddDepartmentInput input);
    }
}
