using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class EditAssessmentDepartmentInput
    {
        [Required]
        public Guid? DepartmentId { get; set; }
        [Required]
        public Guid? AssessmentId { get; set; }
    }
}
