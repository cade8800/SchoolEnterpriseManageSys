using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Enterprise.Dto
{
    public class GetEnterpriseDetailOutput : EnterpriseOutput
    {
        public List<EnterpriseFileDto> FileList { get; set; } = new List<EnterpriseFileDto>();
    }
}
