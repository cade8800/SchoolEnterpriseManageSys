using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class GetAssessmentDepartmentIndexListOutput
    {
        public List<AssessmentDepartmentIndexOutput> AssessmentDepartmentIndexOutputs { get; set; } = new List<AssessmentDepartmentIndexOutput>();
    }
}
