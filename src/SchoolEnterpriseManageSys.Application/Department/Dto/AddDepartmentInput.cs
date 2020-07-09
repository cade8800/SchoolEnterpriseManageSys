using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Department.Dto
{
    public class AddDepartmentInput
    {
        [Required, StringLength(32)]
        public string Name { get; set; }
    }
}
