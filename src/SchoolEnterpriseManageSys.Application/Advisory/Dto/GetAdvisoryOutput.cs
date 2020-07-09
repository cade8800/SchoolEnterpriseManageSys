using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Advisory.Dto
{
    public class GetAdvisoryOutput
    {
        public List<AdvisoryDto> AdvisoryList { get; set; } = new List<AdvisoryDto>();
    }
}
