using SchoolEnterpriseManageSys.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class GetCollectOutput : BasePagingOutput
    {
        public List<CollectOutput> CollectList { get; set; } = new List<CollectOutput>();
    }
}
