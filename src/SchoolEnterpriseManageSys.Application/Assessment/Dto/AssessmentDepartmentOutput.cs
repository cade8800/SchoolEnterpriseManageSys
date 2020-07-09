using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class AssessmentDepartmentOutput
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 考核标识
        /// </summary>
        public Guid? AssessmentId { get; set; }
        /// <summary>
        /// 系标识
        /// </summary>
        public Guid? DepartmentId { get; set; }



        public string DepartmentName { get; set; }
        public decimal TotalScore { get; set; }
        public decimal SinceScore { get; set; }
        public decimal ExpertScore { get; set; }

    }
}
