using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class GetGetSummaryOutput
    {
        public List<RankingOutput> RankingList { get; set; } = new List<RankingOutput>();
        public List<TimelineOutput> TimelineList { get; set; } = new List<TimelineOutput>();
    }
}
