using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class InsertJointlyEstablishedProfessionInput : JointlyEstablishedProfessionDto
    {
        public List<Guid> FileIdList { get; set; } = new List<Guid>();
    }
}
