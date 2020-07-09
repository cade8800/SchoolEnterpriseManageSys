using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class GetAssessmentInput
    {
        [Required]
        public Guid? Id { get; set; }
    }
}
