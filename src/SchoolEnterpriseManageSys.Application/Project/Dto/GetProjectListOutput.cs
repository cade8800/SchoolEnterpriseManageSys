using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class GetProjectListOutput : BaseDto.BasePagingOutput
    {
        public List<ProjectOutput> ProjectList { get; set; } = new List<ProjectOutput>();
    }
}
