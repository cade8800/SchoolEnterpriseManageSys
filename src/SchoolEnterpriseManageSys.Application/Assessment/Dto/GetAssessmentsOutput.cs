using SchoolEnterpriseManageSys.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class GetAssessmentsOutput : BasePagingOutput
    {
        public List<AssessmentDto> AssessmentList { get; set; } = new List<AssessmentDto>();
    }
}
