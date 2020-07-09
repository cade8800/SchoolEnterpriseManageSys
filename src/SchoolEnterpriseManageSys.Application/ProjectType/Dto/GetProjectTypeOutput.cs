using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.ProjectType.Dto
{
    public class GetProjectTypeOutput
    {
        public List<ProjectTypeDto> Types { get; set; } = new List<ProjectTypeDto>();
    }
}
