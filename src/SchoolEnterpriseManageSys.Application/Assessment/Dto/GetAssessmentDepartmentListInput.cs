using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class GetAssessmentDepartmentListInput
    {
        [Required]
        public Guid? AssessmentId { get; set; }
    }
}
