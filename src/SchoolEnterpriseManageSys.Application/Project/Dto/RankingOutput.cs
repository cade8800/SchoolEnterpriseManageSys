using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class RankingOutput
    {
        public int Total { get; set; }

        public Enum.ProjectType? Type { get; set; }
        public string Title
        {
            get
            {
                return this.Type.HasValue ? SchoolEnterpriseManageSys.Utilities.EnumHelper.EnumExtensions.GetDescription(this.Type) : "";
            }
        }
    }
}
