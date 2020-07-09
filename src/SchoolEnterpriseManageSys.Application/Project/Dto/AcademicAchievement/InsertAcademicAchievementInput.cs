using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class InsertAcademicAchievementInput : AcademicAchievementDto
    {
        public List<Guid> FileIdList { get; set; } = new List<Guid>();
    }
}
