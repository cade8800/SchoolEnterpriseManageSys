using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class UpdateCooperationInput
    {
        [Required]
        public Guid? Id { get; set; }

        /// <summary>
        /// 学术机构数量
        /// </summary>
        public int AcademicAgencyCount { get; set; }
        /// <summary>
        /// 行业或企业机构数量
        /// </summary>
        public int EnterpriseCount { get; set; }
        /// <summary>
        /// 地方政府数量
        /// </summary>
        public int LocalGovernmentCount { get; set; }
    }
}
