using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Department.Dto
{
    public class GetDepartmentOutput
    {
        public List<DepartmentDto> DepartmentList { get; set; } = new List<DepartmentDto>();
    }
}
