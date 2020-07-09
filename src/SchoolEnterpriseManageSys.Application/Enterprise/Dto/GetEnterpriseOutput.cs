using SchoolEnterpriseManageSys.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Enterprise.Dto
{
    public class GetEnterpriseOutput : BasePagingOutput
    {
        public List<GetEnterpriseDetailOutput> Enterprises { get; set; } = new List<GetEnterpriseDetailOutput>();
    }
}
