using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class GetAssessmentDepartmentListOutput
    {
        public List<AssessmentDepartmentOutput> AssessmentDepartmentOutputs { get; set; } = new List<AssessmentDepartmentOutput>();
    }
}
