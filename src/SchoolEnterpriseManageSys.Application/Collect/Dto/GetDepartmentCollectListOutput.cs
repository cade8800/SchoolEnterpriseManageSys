using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class GetDepartmentCollectListOutput
    {
        public List<DepartmentCollectOutput> DepartmentCollectList { get; set; } = new List<DepartmentCollectOutput>();
    }
}
