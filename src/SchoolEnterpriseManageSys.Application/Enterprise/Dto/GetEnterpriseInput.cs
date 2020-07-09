using SchoolEnterpriseManageSys.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Enterprise.Dto
{
    public class GetEnterpriseInput : BasePagingInput
    {
        public string Keyword { get; set; }

        public bool? IsWithFileInfo { get; set; }
    }
}
