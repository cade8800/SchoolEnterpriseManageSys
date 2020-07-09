using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class GetAssessmentDepartmentIndexListInput
    {
        [Required]
        public Guid? AssessmentDepartmentId { get; set; }
    }
}
