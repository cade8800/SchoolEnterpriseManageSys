using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.File.Dto
{
    public class ProjectFileInput
    {
        [Required]
        public Guid? ProjectId { get; set; }
        [Required]
        public Guid? FileId { get; set; }
    }
}
