using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class GetProjectDetailOutput : ProjectOutput
    {
        public List<ProjectFileOutput> FileList { get; set; } = new List<ProjectFileOutput>();
    }
}
