using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class GetProjectListInput : BaseDto.BasePagingInput
    {
        public string Keyword { get; set; }
        public Enum.ProjectType? Type { get; set; }
    }
}
