using SchoolEnterpriseManageSys.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Advisory.Dto
{
    public class GetEnterpriseAdvisoryOutput : BasePagingOutput
    {
        public List<AdvisoryDto> AdvisoryList { get; set; } = new List<AdvisoryDto>();
    }
}
